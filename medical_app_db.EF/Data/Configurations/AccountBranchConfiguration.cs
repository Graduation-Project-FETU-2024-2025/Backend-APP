using medical_app_db.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace medical_app_db.EF.Data.Configurations
{
    public class AccountBranchConfiguration : IEntityTypeConfiguration<AccountBranch>
    {
        public void Configure(EntityTypeBuilder<AccountBranch> builder)
        {
            builder.HasKey(a => new { a.AccountId , a.BranchId });

            builder.HasOne(a => a.Account).WithMany(p => p.AccountBranches)
                .HasForeignKey(ab => ab.AccountId)
                .HasPrincipalKey(a => a.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Branch).WithMany(p => p.AccountBranches)
                .HasForeignKey(ab => ab.BranchId)
                .HasPrincipalKey(b => b.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
