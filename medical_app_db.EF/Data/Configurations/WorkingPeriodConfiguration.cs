using medical_app_db.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace medical_app_db.EF.Data.Configurations
{
    public class WorkingPeriodConfiguration : IEntityTypeConfiguration<WorkingPeriod>
    {
        public void Configure(EntityTypeBuilder<WorkingPeriod> builder)
        {
            builder.HasKey(wp => new { wp.BranchId, wp.Start , wp.End });
            builder.Property(wp => wp.BranchId).IsRequired();
            builder.Property(wp => wp.Start).IsRequired();
            builder.Property(wp => wp.End).IsRequired();

            builder.HasOne(wp => wp.Branch).WithMany(b => b.WorkingPeriods).HasForeignKey(wp => wp.BranchId);
        }
    }
}
