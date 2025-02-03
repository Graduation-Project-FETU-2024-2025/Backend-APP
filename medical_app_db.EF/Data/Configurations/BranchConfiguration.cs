using medical_app_db.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace medical_app_db.EF.Data.Configurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasKey(b => new { b.Id, b.PharmacyId });

            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.Property(b => b.PharmacyId).IsRequired();
            builder.Property(b => b.Lat).IsRequired();
            builder.Property(b => b.Long).IsRequired();
            builder.Property(b => b.Status).IsRequired();
            builder.Property(b => b.MinDeliveryPrice).HasPrecision(18, 2);
            builder.Property(b => b.PricePerKilo).HasPrecision(18, 2);
            builder.Property(b => b.AR_BranchName).HasMaxLength(100).IsRequired();
            builder.Property(b => b.EN_BranchName).HasMaxLength(100).IsRequired();
            builder.Property(b => b.Image).HasMaxLength(255);
            builder.Property(b => b.PhoneNumber).HasMaxLength(15).IsRequired();

            builder.HasMany(b => b.WorkingPeriods)
                   .WithOne(wp => wp.Branch)
                   .HasForeignKey(wp => wp.BranchId);

            builder.HasOne(b => b.Pharmacy)
                   .WithMany(p => p.Branches)
                   .HasForeignKey(b => b.PharmacyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
