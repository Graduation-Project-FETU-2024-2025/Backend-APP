using medical_app_db.Core.Helpers;
using medical_app_db.Core.Interfaces;
using medical_app_db.EF.Services;

namespace medical_app_api.Extentions
{
    public static class EmailExtentions
    {
        public static IServiceCollection AddEmailConfiguration(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.Configure<EmailSetting>(configuration.GetSection("EmailConfiguration"));
            return services;
        }

        public static IServiceCollection AddEmailService(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
