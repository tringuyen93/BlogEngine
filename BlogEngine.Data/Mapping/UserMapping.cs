using System;
using System.Collections.Generic;
using System.Text;
using BlogEngine.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogEngine.Data.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("sys.Users");
            builder.HasMany(x => x.Claims).WithOne().HasForeignKey(x => x.UserId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Roles).WithOne().HasForeignKey(x => x.UserId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
