using medical_app_db.Core.Models.Order_Module;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace medical_app_db.Core.Models
{
    public class User : ApplicationUser
    {

        public ICollection<Order>? Orders { get; set; }
    }
}


