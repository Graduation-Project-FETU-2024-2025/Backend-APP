namespace medical_app_db.Core.DTOs
{
    public class SystemProductWithBranchInfoDTO
    {
        public string PharmacyName { get; set; } = null!;
        public string BranchName { get; set; } = null!;
        public string BranchLocation { get; set; } = null!;
        public int Stock { get; set; }
        public float Price { get; set; }
        public float DeliveryPrice { get; set; }
    }

}
