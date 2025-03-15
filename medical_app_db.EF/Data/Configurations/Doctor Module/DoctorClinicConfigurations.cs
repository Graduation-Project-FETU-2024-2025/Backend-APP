using medical_app_db.Core.Models.Doctor_Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace medical_app_db.EF.Data.Configurations.Doctor_Module
{
    internal class DoctorClinicConfigurations : IEntityTypeConfiguration<DoctorClinic>
    {
        public void Configure(EntityTypeBuilder<DoctorClinic> builder)
        {
            builder.HasKey(dc => new { dc.ClinicId, dc.DoctorId });

            builder.HasOne(dc => dc.Doctor)
                .WithOne(d => d.DoctorClinic)
                .HasForeignKey<DoctorClinic>(dc => dc.DoctorId);

            builder.HasOne(dc => dc.Clinic)
                .WithOne(d => d.DoctorClinic)
                .HasForeignKey<DoctorClinic>(dc => dc.ClinicId);
        }
    }
}
