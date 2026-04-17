using Fitness_Tracker.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fitness_Tracker.Areas.Admin.Controllers;

[Area("Admin")]
[Route("admin/orders")]
[Authorize(Roles = "Admin")]
public class OrdersController : Controller
{
    private readonly IDatabaseAgent _agent;
    private readonly UserManager<IdentityUser> _userManager;

    public OrdersController(IDatabaseAgent agent, UserManager<IdentityUser> userManager)
    {
        _agent = agent;
        _userManager = userManager;
    }

    // Admin-only view of an order. (Does not rely on the current user's cart.)
    // GET /admin/orders/confirmation?orderId=123
    [HttpGet("confirmation")]
    public async Task<IActionResult> Confirmation(int orderId)
    {
        var userId = _userManager.GetUserId(User);
        if (string.IsNullOrEmpty(userId))
        {
            return Challenge();
        }

        // Keep it simple: reuse existing agent method.
        // If you want admins to view ANY user's order, add an agent method that ignores userId.
        var order = await _agent.GetOrderByIdForUserAsync(orderId, userId);
        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }
}
