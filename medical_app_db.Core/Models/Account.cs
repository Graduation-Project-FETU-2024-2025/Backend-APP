using Microsoft.AspNetCore.Identity;

namespace medical_app_db.Core.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public Guid PharmacyId { get; set; }
        public Pharmacy Pharmacy { get; set; } = null!;
        public ApplicationUser? ApplicationUser { get; set; }
        public Guid ApplicationUserId { get; set; }
        public string? Image { get; set; }

        public ICollection<AccountBranch>? AccountBranches { get; set; }
    }
}
