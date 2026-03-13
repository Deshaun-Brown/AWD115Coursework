using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Fitness_Tracker.Data;
using System.Threading.Tasks;

namespace Fitness_Tracker.Controllers;

[Authorize]
[Route("cart")]
public class CartController : Controller
{
    private readonly IDatabaseAgent _agent;
    private readonly UserManager<IdentityUser> _userManager;

    public CartController(IDatabaseAgent agent, UserManager<IdentityUser> userManager)
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
        return View(items);
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
        if (Request.Headers.ContainsKey("Referer"))
        {
            return Redirect(Request.Headers["Referer"].ToString());
        }
        return RedirectToAction("Index");
    }
}
