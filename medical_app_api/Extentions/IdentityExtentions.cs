using medical_app_db.Core.Models;
using medical_app_db.EF.Data;
using Microsoft.AspNetCore.Identity;

namespace medical_app_api.Extentions
{
    public static class IdentityExtentions
    {
        public static IServiceCollection InjectIdentityCore<T>(this IServiceCollection services) where T : class
        {
            services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<MedicalDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }
        public static IServiceCollection InjectIdentity<T>(this IServiceCollection services) where T : class
        {
            services.AddIdentity<T, IdentityRole<Guid>>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<MedicalDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }
    }
}
