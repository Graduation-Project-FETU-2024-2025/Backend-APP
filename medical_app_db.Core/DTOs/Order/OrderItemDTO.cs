namespace medical_app_db.Core.DTOs.Order
{
    public class OrderItemDTO
    {
        public Guid SystemProductCode { get; set; }
        public decimal SystemProductPrice { get; set; }
        public string? SystemProductImage { get; set; }
        public string? SystemProductName { get; set; }
        public int Quantity { get; set; }
    }
}
