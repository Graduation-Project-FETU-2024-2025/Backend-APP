using medical_app_db.Core.Models.Doctor_Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace medical_app_db.EF.Data.Configurations.Doctor_Module
{
    internal class ClinicConfigurations : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Price)
     .HasColumnType("decimal(18,2)"); 


            builder.HasMany(c => c.ClinicPhones)
                .WithOne(cp => cp.Clinic)
                .HasForeignKey(cp => cp.C_ID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Appointments)
                .WithOne(a => a.Clinic)
                .HasForeignKey(a => a.ClinicId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.AppointmentDates)
                .WithOne(a => a.Clinic)
                .HasForeignKey(a => a.ClinicId)
                .OnDelete(DeleteBehavior.Cascade);

       
        }
    }
}
