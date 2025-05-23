﻿using medical_app_db.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace medical_app_db.EF.Data.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            
            builder
              .HasIndex(a => new { a.Id, a.PharmacyId })
              .IsUnique();
            builder.Property(a => a.PharmacyId).IsRequired();

            builder.HasOne(a => a.Pharmacy)
                .WithMany(p => p.Accounts)
                .HasForeignKey(a => a.PharmacyId);

            builder
                .HasOne(a => a.Pharmacy)
                .WithMany(p => p.Accounts)
                .HasForeignKey(a => a.PharmacyId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
