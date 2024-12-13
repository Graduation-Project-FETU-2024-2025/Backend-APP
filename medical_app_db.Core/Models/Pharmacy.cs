namespace medical_app_db.Core.Models
{
    public class Pharmacy
    {
        public Guid Id { get; set; }
        public string ArName { get; set; } = null!;
		public string? EnName { get; set; }
		public string? Logo { get; set; }
		public virtual ICollection<Branch> Branches { get; set; } = null!;
		public virtual ICollection<Account> Accounts { get; set; } = null!;
	}
}
