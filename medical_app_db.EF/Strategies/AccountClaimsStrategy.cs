using medical_app_db.Core.Interfaces;
using medical_app_db.Core.Models;
using System.Security.Claims;

namespace medical_app_db.EF.Strategies
{
    public class AccountClaimsStrategy : IUserClaimsStrategy
    {
        public List<Claim> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>();
            if (user is Account account)
            {
                claims.Add(new Claim("Pharmacy", account.PharmacyId.ToString() ?? ""));
                claims.Add(new Claim("Account", account.Id.ToString() ?? ""));

            }
            return claims;
        }
    }
}
