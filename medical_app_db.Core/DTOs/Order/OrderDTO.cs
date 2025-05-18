namespace medical_app_db.Core.DTOs.Order
{
    public class OrderDTO
    {
        public required string UserName { get; set; }
        public required string UserEmail { get; set; }
        public required string UserAddress { get; set; }
        public Guid BranchId { get; set; }
        public required double UserLat { get; set; }
        public required double UserLong { get; set; }

        public required ICollection<OrderItemDTO> OrderItems { get; set; }
    }
}
