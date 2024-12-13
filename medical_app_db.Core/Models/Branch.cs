namespace medical_app_db.Core.Models
{
    public class Branch
    {
        public Guid Id { get; set; }
        public Guid PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; } = null!;
		public double Long { get; set; }
        public double Lat { get; set; }
        public int DeliveryRange { get; set; }
        public decimal PricePerKilo { get; set; }
        public decimal MinDeliveryPrice { get; set; }
        public string Status { get; set; } = null!;
		public ICollection<PhoneNumber>? PhoneNumbers { get; set; }
		public ICollection<WorkingPeriod>? WorkingPeriods { get; set; }
		public ICollection<AccountBranch>? AccountBranches { get; set; }
		public ICollection<BranchProduct>? Products { get; set; }

	}
}
