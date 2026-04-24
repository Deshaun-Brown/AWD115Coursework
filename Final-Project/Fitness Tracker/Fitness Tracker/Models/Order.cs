
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness_Tracker.Models
{
    public class Order
    {
        public enum OrderStatus
        {
            Pending,
            Shipped,
            Delivered,
            Cancelled
        }

        // Primary key
        public int OrderId { get; set; }

        public string UserId { get; set; } = string.Empty;

        // Example properties that use the enum
        public string CustomerName { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
