using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using medical_app_db.Core.Models;

namespace medical_app_db.EF.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderId);

            builder.Property(o => o.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.State)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.Date)
                .IsRequired();

            builder.Property(o => o.DeliveryPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.TotalCart)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(o => o.Branch)
                .WithMany()
                .HasForeignKey(o => o.BranchId).HasPrincipalKey(b => b.Id);

            builder.HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId);
        }
    }

}
