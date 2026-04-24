using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmaceuticals.Data;

namespace Pharmaceuticals.Areas.Admin.Controllers;

[Area("Admin")]
[Route("admin/[controller]/[action]")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    


    [HttpGet("")]
    [HttpGet("index")]
    public async Task<IActionResult> Index()
    {
        var totalProducts = await _context.Products.CountAsync();
        var totalCategories = await _context.Categories.CountAsync();
        var totalUsers = await _context.Users.CountAsync();
        var totalOrders = await _context.Orders.CountAsync();

        ViewBag.TotalProducts = totalProducts;
        ViewBag.TotalCategories = totalCategories;
        ViewBag.TotalUsers = totalUsers;
        ViewBag.TotalOrders = totalOrders;

        return View();
    }
}
