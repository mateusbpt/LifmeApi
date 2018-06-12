using Lifme.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Repository.Mapping
{
    public class GroupMapping : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Group", "Lifme");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(255);

            builder.Property(x => x.Image)
                .HasMaxLength(1024);

            builder.HasOne(x => x.Administrator)
               .WithMany()
               .HasForeignKey("AdministratorId")
               .IsRequired();
        }
    }
}
