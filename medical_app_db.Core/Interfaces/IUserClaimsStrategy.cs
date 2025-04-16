using medical_app_db.Core.Models;
using System.Security.Claims;

namespace medical_app_db.Core.Interfaces
{
    public interface IUserClaimsStrategy
    {
        List<Claim> GetClaims(ApplicationUser user);
    }
}
