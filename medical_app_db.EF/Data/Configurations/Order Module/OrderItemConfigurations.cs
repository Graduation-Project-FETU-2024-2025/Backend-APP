using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using medical_app_db.Core.Models;
using medical_app_db.Core.Models.Order_Module;

namespace medical_app_db.EF.Data.Configurations
{
   public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.SystemProductPrice)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");

            builder.Property(i => i.SystemProductImage)
                .IsRequired(false);
            builder.Property(i => i.SystemProductName)
                .IsRequired(true);

            builder.HasOne(i => i.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(i => i.OrderId);
        }
    }

}
