using medical_app_db.Core.DTOs;

namespace medical_app_db.Core.Models
{
    public class Branch
    {
        public Guid Id { get; set; }
        public Guid PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; } = null!;
        public string AR_BranchName { get; set; } = null!;
        public string? EN_BranchName { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
        public int DeliveryRange { get; set; }
        public decimal PricePerKilo { get; set; }
        public decimal MinDeliveryPrice { get; set; }
        public string Status { get; set; } 
        public string? Image { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<WorkingPeriod>? WorkingPeriods { get; set; }
        public ICollection<AccountBranch>? AccountBranches { get; set; }
        public ICollection<BranchProduct>? Products { get; set; }
    }
}
