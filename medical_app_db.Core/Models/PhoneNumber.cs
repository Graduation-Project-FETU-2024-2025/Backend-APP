namespace medical_app_db.Core.Models
{
    public class PhoneNumber
    {
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
