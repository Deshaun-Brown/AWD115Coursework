using Microsoft.AspNetCore.Mvc.Rendering;
using Fitness_Tracker.Models;

namespace Fitness_Tracker.ViewModels;

public class AdminOrderFormViewModel
{
    public int OrderId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public Order.OrderStatus Status { get; set; } = Order.OrderStatus.Pending;

    public List<AdminOrderLineItemViewModel> Items { get; set; } = new();

    public int? SelectedProductId { get; set; }

    public int SelectedQuantity { get; set; } = 1;

    public List<SelectListItem> Products { get; set; } = new();
}
