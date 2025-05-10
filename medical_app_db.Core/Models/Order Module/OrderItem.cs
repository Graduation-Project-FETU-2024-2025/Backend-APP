namespace medical_app_db.Core.Models.Order_Module
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        public Guid SystemProductCode { get; set; }
        public decimal SystemProductPrice { get; set; }
        public string? SystemProductImage { get; set; }
        public required string SystemProductName { get; set; }
        public int Quantity { get; set; }
    }
}