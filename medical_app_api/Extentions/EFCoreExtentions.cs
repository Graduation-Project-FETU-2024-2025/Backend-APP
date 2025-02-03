using medical_app_db.EF.Data;
using Microsoft.EntityFrameworkCore;

namespace medical_app_api.Extentions
{
    public static class EFCoreExtentions
    {
        public static IServiceCollection InjectDbContext(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<MedicalDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}
