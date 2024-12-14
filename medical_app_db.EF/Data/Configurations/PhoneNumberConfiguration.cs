using medical_app_db.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace medical_app_db.EF.Data.Configurations
{
    public class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
    {
        public void Configure(EntityTypeBuilder<PhoneNumber> builder)
        {
            builder.HasKey(pn => new { pn.BranchId, pn.Phone });
            builder.Property(pn => pn.Phone).IsRequired().HasMaxLength(15);
            builder.Property(pn => pn.BranchId).IsRequired();
            
            builder.HasOne(pn => pn.Branch).WithMany(b => b.PhoneNumbers).HasForeignKey(pn => pn.BranchId).HasPrincipalKey(b => b.Id);
        }
    }
}
