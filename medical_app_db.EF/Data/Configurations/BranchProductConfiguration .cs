﻿using medical_app_db.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace medical_app_db.EF.Data.Configurations
{
    public class BranchProductConfiguration : IEntityTypeConfiguration<BranchProduct>
    {
        public void Configure(EntityTypeBuilder<BranchProduct> builder)
        {
            builder.HasKey(a => new { a.BranchId , a.SystemProductCode });

            builder.HasOne(a => a.Branch).WithMany(p => p.Products).HasForeignKey(a => a.BranchId).HasPrincipalKey(b => b.Id);
            builder.HasOne(a => a.SystemProduct).WithMany(p => p.BranchProducts).HasForeignKey(ab => ab.SystemProductCode);
		}
	}
}
