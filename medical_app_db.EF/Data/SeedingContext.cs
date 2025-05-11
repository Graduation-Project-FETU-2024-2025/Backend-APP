using medical_app_db.Core.Models;
using medical_app_db.Core.Models.Doctor_Module;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace medical_app_db.EF.Data
{
    public static class SeedingContext
    {
        public static async Task SeedAsync(MedicalDbContext dbContext, 
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            await SeedRolesAsync(roleManager);
            await SeedSpecializationsAsync(dbContext);
        }
        private static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            string[] roles = { UserRoles.Account, UserRoles.User, UserRoles.Doctor };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }
        }
        private static async Task SeedSpecializationsAsync(MedicalDbContext _dbContext)
        {
            if (_dbContext == null)
                return;
            if (_dbContext.Set<Specialization>().Any())
                return;
            var SpecializationsData = await File.ReadAllTextAsync("../medical_app_db.EF/Data/DataSeed/Specializations.json");
            var Specializations = JsonSerializer.Deserialize<ICollection<Specialization>>(SpecializationsData);
            foreach (var specialization in Specializations ?? [])
            {
                await _dbContext.Set<Specialization>().AddAsync(specialization);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
