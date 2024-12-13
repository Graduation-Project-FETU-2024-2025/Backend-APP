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

            builder.HasOne(a => a.Account).WithMany(p => p.Branches).HasForeignKey(a => a.AccountId);
            builder.HasOne(a => a.Branch).WithMany(p => p.AccountBranches).HasForeignKey(a => a.BranchId);
        }
    }
}
