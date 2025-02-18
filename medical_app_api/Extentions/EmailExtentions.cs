using medical_app_db.Core.Helpers;
using medical_app_db.Core.Interfaces;
using medical_app_db.EF.Services;
using System;

namespace medical_app_api.Extentions
{
    public static class EmailExtentions
    {
        public static IServiceCollection AddEmailConfiguration(
            this IServiceCollection services, 
            IConfiguration configuration,
            IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment.IsDevelopment())
            {
                services.Configure<EmailSetting>(configuration.GetSection("EmailConfiguration")); // in dev
            }
            else if (hostEnvironment.IsProduction())
            {
                services.Configure<EmailSetting>(options =>
                {
                    options.Email = configuration["Email_Confuguration_Email"] ?? "";
                    options.Password = configuration["Email_Configuration_Password"] ?? "";
                    options.Host = configuration["Email_Configuration_Host"] ?? "";
                    options.Port = int.TryParse(configuration["Email_Configuration_Port"], out var port) ? port : 587; // Default port if not set
                    options.EnableSsl = bool.TryParse(configuration["Email_Configuration_EnableSsl"], out var enableSsl) && enableSsl; // Default to true if not set
                });
            }
            return services;
        }

        public static IServiceCollection AddEmailService(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
