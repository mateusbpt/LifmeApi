using Lifme.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lifme.Repository.Mapping
{
    public class PostMapping : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post", "Lifme");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Message)
                .HasMaxLength(1024)
                .IsRequired();

            builder.Property(x => x.Image)
                .HasMaxLength(1024);

            builder.HasOne(x => x.User)
               .WithMany()
               .HasForeignKey("UserId")
               .IsRequired();
        }
    }
}
