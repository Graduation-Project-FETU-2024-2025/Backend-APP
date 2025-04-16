using Microsoft.AspNetCore.Identity;

namespace medical_app_db.Core.Models
{
    public class Account : ApplicationUser
    {
        public Guid PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; } = null!;

        public ICollection<AccountBranch>? AccountBranches { get; set; }
    }
}
