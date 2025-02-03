using medical_app_db.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace medical_app_db.EF.Data.Configurations
{
    public class SystemProductConfiguration : IEntityTypeConfiguration<SystemProduct>
    {
        public void Configure(EntityTypeBuilder<SystemProduct> builder)
        {
            builder.HasKey(a => a.Code);
            builder.Property(a => a.AR_Name).IsRequired();
            builder.Property(a => a.EN_Name).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Image).HasMaxLength(255).IsRequired();
            builder.Property(a => a.Type).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Active_principal).HasMaxLength(100).IsRequired();

        }
    }
}

