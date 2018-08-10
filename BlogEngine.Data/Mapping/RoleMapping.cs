using System;
using System.Collections.Generic;
using System.Text;
using BlogEngine.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogEngine.Data.Mapping
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("sys.Roles");
            builder.HasMany(x => x.Claims).WithOne().HasForeignKey(x => x.RoleId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Users).WithOne().HasForeignKey(x => x.RoleId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
