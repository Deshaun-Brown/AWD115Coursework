using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Fitness_Tracker.Data;
using Fitness_Tracker.ViewModels;
using System.Linq;

namespace Fitness_Tracker.Controllers;

[Authorize]
[Route("orders")] 
public class OrderSummaryController : Controller
{
    private readonly IDatabaseAgent _agent;
    private readonly UserManager<IdentityUser> _userManager;

    public OrderSummaryController(IDatabaseAgent agent, UserManager<IdentityUser> userManager)
    {
        _agent = agent;
        _userManager = userManager;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId)) return RedirectToPage("/Account/Login", new { area = "Identity" });

        var items = await _agent.GetCartItemsForUserAsync(userId);

        var vm = new CartViewModel();
        foreach (var it in items)
        {
            vm.Items.Add(new CartItemViewModel
            {
                CartItemId = it.CartItemId,
                ProductId = it.ProductId,
                Name = it.Product?.Name ?? "",
                Price = it.Product?.Price ?? 0,
                Quantity = it.Quantity,
                ImageUrl = it.Product?.ImageUrl
            });
        }

        return View(vm);
    }
}
