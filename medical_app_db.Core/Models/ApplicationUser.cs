using Microsoft.AspNetCore.Identity;

namespace medical_app_db.Core.Models
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string Name { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public string Picture { get; set; } = null!;
        public string Gnder { get; set; } = null!;
        public string SSN { get; set; } = null!;
        //public string Phone { get; set; } = null!;
    }
}
