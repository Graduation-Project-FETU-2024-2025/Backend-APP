using medical_app_db.EF.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using medical_app_db.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using medical_app_db.Core.Models.Doctor_Module;
using medical_app_db.EF.Data.Configurations.Doctor_Module;

namespace medical_app_db.EF.Data
{
    public class MedicalDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<SystemProduct> SystemProducts { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Branch> Branches { get; set; }

        public DbSet<WorkingPeriod> WorkingPeriods { get; set; }

        public DbSet<BranchProduct> BranchProducts { get; set; }
        public DbSet<AccountBranch> AccountBranches { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<DoctorClinic> DoctorClinics { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<AppointmentDates> AppointmentDates { get; set; }
        public DbSet<ClinicPhone> ClinicPhones { get; set; }
        public DbSet<WorkingPeriodInClinic> WorkingPeriodsInClinics { get; set; }
        public DbSet<PrescriptionProduct> PrescriptionProducts { get; set; }
        public MedicalDbContext(DbContextOptions<MedicalDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BranchConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PharmacyConfiguration).Assembly);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkingPeriodConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SystemProductConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BranchProductConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountBranchConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ItemConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointemntDatesConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointmentConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicConfigurations).Assembly);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicPhoneConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DoctorClinicConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DoctorConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PrescriptionConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PrescriptionProductConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkingPeriodInClinicConfigurations).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}