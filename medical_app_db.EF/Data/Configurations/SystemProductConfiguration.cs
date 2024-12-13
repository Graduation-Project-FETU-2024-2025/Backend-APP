using medical_app_db.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace medical_app_db.EF.Data.Configurations
{
    public class SystemProductConfiguration : IEntityTypeConfiguration<SystemProduct>
    {
        public void Configure(EntityTypeBuilder<SystemProduct> builder)
        {
            builder.HasKey(a => new { a.Code });
            builder.Property(a => a.AR_Name).IsRequired();
        }
    }
}
