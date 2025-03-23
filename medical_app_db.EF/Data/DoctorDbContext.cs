using Microsoft.EntityFrameworkCore;
using medical_app_db.Core.Models;
using medical_app_db.Core.Models.Doctor_Module;
using medical_app_db.EF.Data.Configurations;
using medical_app_db.EF.Data;
using medical_app_db.EF.Data.Configurations.Doctor_Module;

namespace medical_app_db.Infrastructure.Data
{
    public class DoctorDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<DoctorClinic> DoctorClinics { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<AppointmentDates> AppointmentDates { get; set; }
        public DbSet<ClinicPhone> ClinicPhones { get; set; }
        public DbSet<WorkingPeriodInClinic> WorkingPeriodsInClinics { get; set; }
        public DbSet<PrescriptionProduct> PrescriptionProducts { get; set; }

        public DoctorDbContext(DbContextOptions<DoctorDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
