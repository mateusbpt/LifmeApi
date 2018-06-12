using Lifme.Domain.Entity.Relation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Repository.Mapping
{
    public class UserChallengeMapping : IEntityTypeConfiguration<UserChallenge>
    {
        public void Configure(EntityTypeBuilder<UserChallenge> builder)
        {
            builder.ToTable("UserChallenge", "Lifme");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Accept)
                .IsRequired();

            builder.Property(x => x.Completed)
                .IsRequired();

            builder.Property(x => x.DayChallenge)
                .IsRequired();

            builder.Property(x => x.Feedback)
                .HasMaxLength(255);

            builder.HasOne(x => x.Challenge)
                .WithMany()
                .HasForeignKey("ChallengeId");

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey("UserId");
        }
    }
}
