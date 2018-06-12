using Lifme.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Repository.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "Lifme");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email)
                .HasMaxLength(1024)
                .IsRequired();

            builder.Property(x => x.Password)
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(x => x.Name)
              .HasMaxLength(255)
              .IsRequired();

            builder.Property(x => x.LastName)
              .HasMaxLength(255)
              .IsRequired();

            builder.Property(x => x.Nickname)
              .HasMaxLength(255);

            builder.Property(x => x.Avatar)
              .HasMaxLength(1024);

            builder.Property(x => x.Description)
              .HasMaxLength(1024);
        }
    }
}
