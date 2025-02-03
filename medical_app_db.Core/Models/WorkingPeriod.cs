namespace medical_app_db.Core.Models
{
    public class WorkingPeriod
    {
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; } = null!;
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
    }
}
