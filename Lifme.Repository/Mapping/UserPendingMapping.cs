using Lifme.Domain.Entity.Relation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Repository.Mapping
{
    public class UserPendingMapping : IEntityTypeConfiguration<UserPending>
    {
        public void Configure(EntityTypeBuilder<UserPending> builder)
        {
            builder.ToTable("UserPending", "Lifme");

            builder.HasKey(x => new { x.UserId, x.PendingId });

            builder.HasOne(x => x.User)
                  .WithMany(x => x.Pendings)
                  .HasForeignKey(x => x.UserId)
                  .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(x => x.Pending)
                .WithMany()
                .HasForeignKey(x => x.PendingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
