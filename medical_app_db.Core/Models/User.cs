using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace medical_app_db.Core.Models
{
    public class User : IdentityUser<Guid>
    {

        public string Phone { get; set; } = null!;
        public ICollection<Order>? Orders { get; set; }
    }
}


