using medical_app_db.Core.Models.Doctor_Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace medical_app_db.EF.Data.Configurations.Doctor_Module
{
    internal class PrescriptionProductConfigurations : IEntityTypeConfiguration<PrescriptionProduct>
    {
        public void Configure(EntityTypeBuilder<PrescriptionProduct> builder)
        {
            builder.HasKey(pp => new { pp.PrescriptionId, pp.SystemProductCode });

            builder.HasOne(pp => pp.Prescription)
                .WithMany(p => p.PrescriptionProducts)
                .HasForeignKey(pp => pp.PrescriptionId);

            builder.HasOne(pp => pp.SystemProduct).
                WithMany(sp => sp.PrescriptionProducts)
                .HasForeignKey(pp => pp.SystemProductCode);
        }
    }
}
