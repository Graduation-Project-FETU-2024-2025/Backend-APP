using medical_app_db.Core.Models.Doctor_Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace medical_app_db.EF.Data.Configurations.Doctor_Module
{
    public class PrescriptionConfigurations : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Doctor)
                .WithMany(d => d.Prescriptions)
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.Appointment).WithMany().HasForeignKey(p => p.AppointmentId);
            
        }
    }
}
