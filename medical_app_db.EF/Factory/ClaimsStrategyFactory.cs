using medical_app_db.Core.Interfaces;
using medical_app_db.Core.Models.Doctor_Module;
using medical_app_db.Core.Models;
using medical_app_db.EF.Data;
using medical_app_db.EF.Strategies;

namespace medical_app_db.EF.Factory
{
    public class ClaimsStrategyFactory
    {
        private readonly Dictionary<Type, IUserClaimsStrategy> _strategies;

        public ClaimsStrategyFactory(MedicalDbContext context)
        {
            _strategies = new Dictionary<Type, IUserClaimsStrategy>
            {
                { typeof(Account), new AccountClaimsStrategy() },
                { typeof(Doctor), new DoctorClaimsStrategy(context) }
            };
        }

        public IUserClaimsStrategy? GetStrategy(ApplicationUser user)
        {
            return _strategies.ContainsKey(user.GetType()) ? _strategies[user.GetType()] : null;
        }
    }
}
