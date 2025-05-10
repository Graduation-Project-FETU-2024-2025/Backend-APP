using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using medical_app_db.Core.Models.Order_Module;

namespace medical_app_db.EF.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Status)
              .HasConversion(
                  v => v.ToString(),
                  v => Enum.Parse<OrderStatus>(v)
              );

            builder.Property(o => o.OredrDate)
                .IsRequired();

            builder.Property(o => o.DeliveryPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(o => o.Branch)
                .WithMany()
                .HasForeignKey(o => o.BranchId).HasPrincipalKey(b => b.Id);

            builder.HasMany(o => o.OrderItems)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId);
        }
    }

}
