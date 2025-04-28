using medical_app_db.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace medical_app_db.EF.Data.Configurations.Doctor_Module
{
    public class AppointemntDatesConfigurations : IEntityTypeConfiguration<AppointmentDates>
    {
        public void Configure(EntityTypeBuilder<AppointmentDates> builder)
        {
            builder.HasKey(ad => ad.Id);

            builder.HasOne(ad => ad.Clinic)
                .WithMany(c => c.AppointmentDates)
                .HasForeignKey(ad => ad.ClinicId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
