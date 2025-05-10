using medical_app_db.Core.Models.Doctor_Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace medical_app_db.EF.Data.Configurations.Doctor_Module
{
    public class SpecializationConfigurations : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.ArName).IsRequired().HasMaxLength(200);
            builder.Property(s => s.EnName).IsRequired().HasMaxLength(200);
            builder.Property(s => s.Icon).IsRequired(false);
        }
    }
}
