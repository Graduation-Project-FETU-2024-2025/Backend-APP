using medical_app_db.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace medical_app_db.EF.Data.Configurations.Doctor_Module
{
    internal class WorkingPeriodInClinicConfigurations : IEntityTypeConfiguration<WorkingPeriodInClinic>
    {
        public void Configure(EntityTypeBuilder<WorkingPeriodInClinic> builder)
        {
            builder.HasKey(wp => new { wp.AppointmentDateId, wp.StartTime, wp.EndTime });

            builder.HasOne(wp => wp.AppointmentDate)
                .WithMany(ad => ad.WorkingPeriods)
                .HasForeignKey(wp => wp.AppointmentDateId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
