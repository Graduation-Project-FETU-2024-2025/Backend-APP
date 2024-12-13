namespace medical_app_db.Core.Models
{
    public class PhoneNumber
    {
        public Guid BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
