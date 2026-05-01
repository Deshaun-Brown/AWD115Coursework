using Fitness_Tracker.Models;
using Fitness_Tracker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BigBook;

namespace Fitness_Tracker.Areas.Admin.Controllers;

[Area("Admin")]
[Route("admin/orders")]
[Authorize(Roles = "Admin")]
public class OrdersController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public OrdersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var orders = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();

        return View("~/Areas/Admin/Views/Orders/Index.cshtml", orders);
    }

    [HttpGet("details/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        var order = await GetOrderAsync(id);
        return order is null ? NotFound() : View("~/Areas/Admin/Views/Orders/Confirmation.cshtml", order);
    }

    [HttpGet("confirmation/{orderId:int}")]
    public async Task<IActionResult> Confirmation(int orderId)
    {
        var order = await GetOrderAsync(orderId);
        return order is null ? NotFound() : View("~/Areas/Admin/Views/Orders/Confirmation.cshtml", order);
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        var model = await BuildFormAsync();
        return View("~/Areas/Admin/Views/Orders/Create.cshtml", model);
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AdminOrderFormViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.CustomerName))
        {
            ModelState.AddModelError(nameof(model.CustomerName), "Customer name is required.");
        }

        if (model.SelectedProductId is null || model.SelectedProductId <= 0)
        {
            ModelState.AddModelError(nameof(model.SelectedProductId), "Please select a product.");
        }

        if (model.SelectedQuantity < 1)
        {
            ModelState.AddModelError(nameof(model.SelectedQuantity), "Quantity must be at least 1.");
        }

        var product = model.SelectedProductId is null
            ? null
            : await _context.Products.FirstOrDefaultAsync(p => p.ProductId == model.SelectedProductId.Value);

        if (product is null)
        {
            ModelState.AddModelError(nameof(model.SelectedProductId), "Selected product was not found.");
        }

        if (!ModelState.IsValid)
        {
            model.Products = await BuildProductSelectListAsync();
            return View("~/Areas/Admin/Views/Orders/Create.cshtml", model);
        }

        var order = new Order
        {
            UserId = _userManager.GetUserId(User) ?? string.Empty,
            CustomerName = model.CustomerName.Trim(),
            OrderDate = model.OrderDate,
            Status = model.Status,
            OrderItems = new List<OrderItem>
            {
                new()
                {
                    ProductId = product.ProductId,
                    Quantity = model.SelectedQuantity,
                    Price = product.Price
                }
            }
        };

        order.TotalAmount = CalculateTotal(order.OrderItems);

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Order created successfully!";
        return RedirectToAction(nameof(Edit), new { id = order.OrderId });
    }

    [HttpGet("edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var order = await GetOrderAsync(id);
        if (order is null)
        {
            return NotFound();
        }

        var model = await BuildFormAsync(order);
        return View("~/Areas/Admin/Views/Orders/Edit.cshtml", model);
    }

    [HttpPost("edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, AdminOrderFormViewModel model)
    {
        var order = await GetOrderAsync(id);
        if (order is null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(model.CustomerName))
        {
            ModelState.AddModelError(nameof(model.CustomerName), "Customer name is required.");
        }

        if (!ModelState.IsValid)
        {
            model.Products = await BuildProductSelectListAsync();
            model.Items = await BuildLineItemViewModelsAsync(order);
            return View("~/Areas/Admin/Views/Orders/Edit.cshtml", model);
        }

        order.CustomerName = model.CustomerName.Trim();
        order.OrderDate = model.OrderDate;
        order.Status = model.Status;
        order.TotalAmount = CalculateTotal(order.OrderItems);

        await _context.SaveChangesAsync();
        TempData["Success"] = "Order updated successfully!";
        return RedirectToAction(nameof(Edit), new { id = order.OrderId });
    }

    [HttpPost("additem/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddItem(int id, int selectedProductId, int selectedQuantity = 1)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == id);

        if (order is null)
        {
            return NotFound();
        }

        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == selectedProductId);
        if (product is null)
        {
            TempData["Error"] = "Selected product was not found.";
            return RedirectToAction(nameof(Edit), new { id });
        }

        if (selectedQuantity < 1)
        {
            TempData["Error"] = "Quantity must be at least 1.";
            return RedirectToAction(nameof(Edit), new { id });
        }

        var existing = order.OrderItems.FirstOrDefault(oi => oi.ProductId == product.ProductId);
        if (existing is null)
        {
            order.OrderItems.Add(new OrderItem
            {
                ProductId = product.ProductId,
                Quantity = selectedQuantity,
                Price = product.Price
            });
        }
        else
        {
            existing.Quantity += selectedQuantity;
            existing.Price = product.Price;
        }

        order.TotalAmount = CalculateTotal(order.OrderItems);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Item added to order.";
        return RedirectToAction(nameof(Edit), new { id });
    }

    [HttpPost("removeitem/{orderItemId:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveItem(int orderItemId)
    {
        var item = await _context.OrderItems
            .Include(oi => oi.Order)
            .ThenInclude(o => o.OrderItems)
            .FirstOrDefaultAsync(oi => oi.OrderItemId == orderItemId);

        if (item is null || item.Order is null)
        {
            return NotFound();
        }

        var orderId = item.Order.OrderId;
        _context.OrderItems.Remove(item);
        await _context.SaveChangesAsync();

        var order = await GetOrderAsync(orderId);
        if (order is not null)
        {
            order.TotalAmount = CalculateTotal(order.OrderItems);
            await _context.SaveChangesAsync();
        }

        TempData["Success"] = "Item removed from order.";
        return RedirectToAction(nameof(Edit), new { id = orderId });
    }

    [HttpGet("delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await GetOrderAsync(id);
        return order is null ? NotFound() : View("~/Areas/Admin/Views/Orders/Delete.cshtml", order);
    }

    [HttpPost("delete/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == id);

        if (order is null)
        {
            return NotFound();
        }

        _context.OrderItems.RemoveRange(order.OrderItems);
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Order deleted successfully!";
        return RedirectToAction(nameof(Index));
    }

    private async Task<Order?> GetOrderAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.OrderId == id);
    }

    private static decimal CalculateTotal(IEnumerable<OrderItem> items)
    {
        return items.Sum(i => i.Price * i.Quantity);
    }

    private async Task<AdminOrderFormViewModel> BuildFormAsync(Order? order = null)
    {
        return new AdminOrderFormViewModel
        {
            OrderId = order?.OrderId ?? 0,
            CustomerName = order?.CustomerName ?? string.Empty,
            OrderDate = order?.OrderDate ?? DateTime.UtcNow,
            Status = order?.Status ?? Order.OrderStatus.Pending,
            Items = order is null ? new List<AdminOrderLineItemViewModel>() : await BuildLineItemViewModelsAsync(order),
            Products = await BuildProductSelectListAsync()
        };
    }

    private async Task<List<AdminOrderLineItemViewModel>> BuildLineItemViewModelsAsync(Order order)
    {
        return await Task.FromResult(order.OrderItems
            .Select(i => new AdminOrderLineItemViewModel
            {
                OrderItemId = i.OrderItemId,
                ProductId = i.ProductId,
                ProductName = i.Product?.Name ?? string.Empty,
                Quantity = i.Quantity,
                Price = i.Price
            })
            .ToList());
    }

    private async Task<List<SelectListItem>> BuildProductSelectListAsync()
    {
        return await _context.Products
            .OrderBy(p => p.Name)
            .Select(p => new SelectListItem
            {
                Value = p.ProductId.ToString(),
                Text = $"{p.Name} ({p.Price:C})"
            })
            .ToListAsync();
    }
}
