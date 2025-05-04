using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using medical_app_db.Core.Models;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(e => e.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId);
    }
}
