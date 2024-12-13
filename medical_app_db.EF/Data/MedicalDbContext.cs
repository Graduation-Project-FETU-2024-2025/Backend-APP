
using Microsoft.EntityFrameworkCore;
using medical_app_db.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace medical_app_db.EF.Data
{
    public class MedicalDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<SystemProduct> SystemProducts { get; set; }

        public MedicalDbContext(DbContextOptions<MedicalDbContext> options) : base(options)
        {
        }

      
    }
}
