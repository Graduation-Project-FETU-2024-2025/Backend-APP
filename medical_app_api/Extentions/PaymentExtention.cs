using medical_app_db.Core.Helpers;

namespace medical_app_api.Extentions
{
    public static class PaymentExtention
    {
        public static IServiceCollection AddPaymentConfiguration(
            this IServiceCollection services, 
            IConfiguration configuration,
            IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment.IsDevelopment())
            {
                services.Configure<PaymobSetting>(configuration.GetSection("Paymob")); // in dev
            }
            else if (hostEnvironment.IsProduction())
            {
                services.Configure<PaymobSetting>(options =>
                {
                    options.ApiKey = configuration["Paymob_ApiKey"] ?? "";
                    options.HmacSecret = configuration["Paymob_Hmac"] ?? "";
                    options.IntegrationId = configuration["Paymob_IntegrationId"];
                    options.IFrameId = configuration["Paymob_IFrameId"];
                });
            }
            return services;
        }
    }
}
