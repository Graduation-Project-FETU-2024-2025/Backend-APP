using medical_app_db.Core.Helpers;
using medical_app_db.Core.Interfaces;
using medical_app_db.EF.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace medical_app_api.Extentions
{
    public static class AuthenticationExtentions
    {
        public static IServiceCollection AddJWTConfiguration(
            this IServiceCollection services , 
            IConfiguration configuration)
        {
            services.Configure<JWT>(configuration.GetSection("JWT"));
            return services;

        }
        public static IServiceCollection AddJWTAuth(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = false;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudience = configuration["JWT:AudienceIP"],
                    ValidIssuer = configuration["JWT:IssureIP"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["JWT:SecurityKey"] ?? "hasjkhdjaskhda")
                        )
                };
            });
            return services;
        }  
        public static IServiceCollection AddAuthService(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
