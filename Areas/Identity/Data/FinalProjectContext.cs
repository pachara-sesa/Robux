﻿using AS3_1660706126.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AS3_1660706126.Data
{
    public class FinalProjectContext : IdentityDbContext<FinalProjectUser>
    {
        public FinalProjectContext(DbContextOptions<FinalProjectContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Apply custom configurations for FinalProjectUser
            builder.ApplyConfiguration(new FinalProjectUserEntityConfiguration());
        }
    }

    public class FinalProjectUserEntityConfiguration : IEntityTypeConfiguration<FinalProjectUser>
    {
        public void Configure(EntityTypeBuilder<FinalProjectUser> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(255);
            builder.Property(u => u.LastName).HasMaxLength(255);
            builder.Property(u => u.MobilePhone).HasMaxLength(255);
        }
    }
}
