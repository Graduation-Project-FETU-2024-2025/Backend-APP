using medical_app_db.Core.Models.Order_Module;

namespace medical_app_db.Core.DTOs.Order
{
    public class OrderToReturnDTO
    {
        public Guid Id { get; set; }
        public DateTime OredrDate { get; set; } = DateTime.UtcNow;
        public string? Status { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserAddress { get; set; }
        public string? Ar_BranchName { get; set; }
        public string? En_BranchName { get; set; }

        public required ICollection<OrderItemToReturnDTO> OrderItems { get; set; }
    }
}
