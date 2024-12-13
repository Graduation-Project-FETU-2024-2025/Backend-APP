using Microsoft.AspNetCore.Identity;

namespace medical_app_db.Core.Models
{
    public class Account:IdentityUser<Guid>
    {
        public Guid PharmacyId { get; set; }
        public virtual Pharmacy Pharmacy { get; set; }
        public string Image { get; set; }
    }
}
