using Lifme.Domain.Entity.Relation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Repository.Mapping
{
    public class UserBadgeMapping : IEntityTypeConfiguration<UserBadge>
    {
        public void Configure(EntityTypeBuilder<UserBadge> builder)
        {
            builder.ToTable("UserBadge", "Lifme");

            builder.HasKey(x => new { x.UserId, x.BadgeId });

            builder.HasOne(x => x.User)
                  .WithMany(x => x.Badges)
                  .HasForeignKey(x => x.UserId)
                  .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(x => x.Badge)
                .WithMany()
                .HasForeignKey(x => x.BadgeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
