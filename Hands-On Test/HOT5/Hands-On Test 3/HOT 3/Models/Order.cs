using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pharmaceuticals.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        
        public string UserId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; } = string.Empty;

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        public OrderStatus Status { get; set; }

        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public enum OrderStatus
        {
            Pending,
            Processing,
            Shipped,
            Delivered,
            Cancelled
        }
    }
}
