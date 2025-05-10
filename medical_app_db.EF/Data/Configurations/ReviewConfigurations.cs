using medical_app_db.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace medical_app_db.EF.Data.Configurations
{
    public class ReviewConfigurations : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasOne(r => r.User).WithMany().HasForeignKey(r => r.UserId);
            builder.HasOne(r => r.Clinic).WithMany().HasForeignKey(r => r.ClinicId);
        }
    }
}
