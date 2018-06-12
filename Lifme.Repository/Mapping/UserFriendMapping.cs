using Lifme.Domain.Entity.Relation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Repository.Mapping
{
    public class UserFriendMapping : IEntityTypeConfiguration<UserFriend>
    {
        public void Configure(EntityTypeBuilder<UserFriend> builder)
        {
            builder.ToTable("UserFriend", "Lifme");

            builder.HasKey(x => new { x.UserId, x.FriendId });

            builder.HasOne(x => x.User)
                  .WithMany(x => x.Friends)
                  .HasForeignKey(x => x.UserId)
                  .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(x => x.Friend)
                .WithMany()
                .HasForeignKey(x => x.FriendId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
