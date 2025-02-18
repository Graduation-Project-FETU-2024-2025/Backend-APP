using Microsoft.AspNetCore.Identity;

namespace medical_app_db.Core.Models
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public Account? Account { get; set; }
        public User? User { get; set; }
    }
}
