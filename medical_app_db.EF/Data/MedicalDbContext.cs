using medical_app_db.EF.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.EF.Data
{
    public class MedicalDbContext : DbContext
    {
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
