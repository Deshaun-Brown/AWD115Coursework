using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pharmaceuticals.Data;
using Pharmaceuticals.Models;
using Pharmaceuticals.ViewModels;

namespace Pharmaceuticals.Controllers;

[Authorize]
[Route("cart")]
public class CartController : Controller
{
    private readonly IDatabaseAgent _agent;
    private readonly UserManager<ApplicationUser> _userManager;

    public CartController(IDatabaseAgent agent, UserManager<ApplicationUser> userManager)
    {
        _agent = agent;
        _userManager = userManager;
    }

    // POST /cart/add
    [HttpPost("add")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId))
        {
            return Challenge();
        }

        await _agent.AddToCartAsync(userId, productId, quantity);
        TempData["Success"] = "Item added to cart successfully.";

        // Redirect back to the referrer or to the browse page
        if (Request.Headers.ContainsKey("Referer"))
        {
            return Redirect(Request.Headers["Referer"].ToString());
        }
        return RedirectToAction("Browse", "Order");
    }

    // GET /cart (simple view of cart items)
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId)) return RedirectToPage("/Account/Login", new { area = "Identity" });

        var items = await _agent.GetCartItemsForUserAsync(userId);

        var model = new CartViewModel
        {
            Items = items,
            TotalQuantity = items.Sum(i => i.Quantity),
            TotalPrice = items.Sum(i => (i.Product?.Price ?? 0m) * i.Quantity)
        };

        return View(model);
    }

    // GET /cart/count - returns JSON with total quantity
    [HttpGet("count")]
    [AllowAnonymous]
    public async Task<IActionResult> Count()
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId)) return Ok(new { count = 0 });

        var items = await _agent.GetCartItemsForUserAsync(userId);
        var total = items.Sum(i => i.Quantity);
        return Ok(new { count = total });
    }

    // POST /cart/remove
    [HttpPost("remove")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(int cartItemId)
    {
        await _agent.RemoveCartItemAsync(cartItemId);
        TempData["Success"] = "Item removed from cart.";
        if (Request.Headers.ContainsKey("Referer"))
        {
            return Redirect(Request.Headers["Referer"].ToString());
        }
        return RedirectToAction("Index");
    }

    [HttpPost("checkout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout()
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId))
        {
            return Challenge();
        }

        var order = await _agent.CheckoutAsync(userId);
        if (order == null)
        {
            return RedirectToAction("Index");
        }

        return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
    }

    [HttpGet("orderconfirmation")]
    public async Task<IActionResult> OrderConfirmation(int orderId)
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId))
        {
            return Challenge();
        }

        var order = await _agent.GetOrderByIdForUserAsync(orderId, userId);

        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }
}