using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using medical_app_db.Core.Models;

namespace medical_app_db.EF.Data.Configurations
{
   public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(i => new { i.OrderId, i.Code });

            builder.Property(i => i.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(i => i.Image)
                .IsRequired(false);

            builder.HasOne(i => i.SystemProduct)
                .WithMany()
                .HasForeignKey(i => i.Code);
        }
    }

}
