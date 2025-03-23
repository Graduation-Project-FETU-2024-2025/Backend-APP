using medical_app_db.Core.Models.Doctor_Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace medical_app_db.EF.Data.Configurations.Doctor_Module
{
    public class AppointmentConfigurations : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Price).HasColumnType("decimal(18,2)");
            builder.Property(a => a.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => Enum.Parse<AppointmentStatus>(v)
                );
            builder.HasOne(a => a.Clinic).WithMany(c => c.Appointments).HasForeignKey(a => a.ClinicId);
            builder.HasOne(a => a.User).WithMany().HasForeignKey(a => a.UserId);
        }
    }
}
