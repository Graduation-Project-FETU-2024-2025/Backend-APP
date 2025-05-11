using medical_app_db.EF.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace medical_app_api.Extentions
{
    public static class SeedingExtention
    {
        public static async Task<IApplicationBuilder> SeedAsync(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var _dbContext = services.GetRequiredService<MedicalDbContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(typeof(Program));
            try
            {
                await _dbContext.Database.MigrateAsync();
                await SeedingContext.SeedAsync(_dbContext,roleManager);
            }
            catch(Exception ex) 
            {
                logger.LogError(message: ex.Message);
            }
            return app;
        }
    }
}
