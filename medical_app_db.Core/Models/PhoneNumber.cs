namespace medical_app_db.Core.Models
{
    public class PhoneNumber
    {
        public Guid BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public string Phone { get; set; }
    }
}
