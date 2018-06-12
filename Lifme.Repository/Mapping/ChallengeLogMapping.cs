using Lifme.Domain.Entity.Log;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Repository.Mapping
{
    public class ChallengeLogMapping : IEntityTypeConfiguration<ChallengeLog>
    {
        public void Configure(EntityTypeBuilder<ChallengeLog> builder)
        {
            builder.ToTable("ChallengeLog", "Lifme");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Accept)
                .IsRequired();

            builder.Property(x => x.Date)
                    .IsRequired();

            builder.HasOne(x => x.Challenge)
                    .WithMany()
                    .HasForeignKey("ChallengeId")
                    .IsRequired();
        }
    }
}

