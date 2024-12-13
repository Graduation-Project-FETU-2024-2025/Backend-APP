using medical_app_db.EF.Data.Configurations;

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
        public DbSet<SystemProduct> SystemProducts { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<WorkingPeriod> WorkingPeriods { get; set; }

        public MedicalDbContext(DbContextOptions<MedicalDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BranchConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PharmacyConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PhoneNumberConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkingPeriodConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }

      
    }
}
