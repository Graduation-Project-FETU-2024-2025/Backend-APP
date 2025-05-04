using medical_app_db.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace medical_app_db.EF.Data.Configurations.Doctor_Module
{
    public class ClinicPhoneConfigurations : IEntityTypeConfiguration<ClinicPhone>
    {
        public void Configure(EntityTypeBuilder<ClinicPhone> builder)
        {
            builder.HasKey(cp => new { cp.C_ID, cp.PhoneNumber });
            
        }
    }
}
