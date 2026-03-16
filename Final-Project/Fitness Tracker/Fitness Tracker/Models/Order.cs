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

        // Example properties that use the enum
        public string CustomerName { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
    }
}
