namespace medical_app_db.Core.Models.Order_Module
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime OredrDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public required string Name { get; set; }
        public required string UserEmail { get; set; }
        public required string UserAddress { get; set; }
        public Guid BranchId { get; set; }
        public Branch? Branch { get; set; }

        public required ICollection<OrderItem> OrderItems { get; set; }
    }
}
