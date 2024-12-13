namespace medical_app_db.Core.Models
{
    public class Pharmacy
    {
        public Guid Id { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public string Logo { get; set; }
        public ICollection<Branch> Branches { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
