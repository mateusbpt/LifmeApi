using Lifme.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Repository.Mapping
{
    public class TournamentMapping : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.ToTable("Tournament", "Lifme");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Image)
                .HasMaxLength(1024);

            builder.Property(x => x.StartDate)
                .IsRequired();

            builder.Property(x => x.Feedback)
                .HasMaxLength(255);

            builder.HasOne(x => x.Winner)
               .WithMany()
               .HasForeignKey("WinnerId");

            builder.HasOne(x => x.Group)
                .WithMany()
                .HasForeignKey("GroupId");

        }
    }
}
