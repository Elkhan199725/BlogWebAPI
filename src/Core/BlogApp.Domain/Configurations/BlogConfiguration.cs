using BlogApp.Domain.Models.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Configurations;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.ToTable("Blogs");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(b => b.Description)
               .IsRequired();

        builder.Property(b => b.CreatedDate)
               .IsRequired();

        builder.Property(b => b.ModifiedDate)
               .IsRequired(false);

        builder.Property(b => b.ModifiedBy)
               .HasMaxLength(50)
               .IsRequired(false);
    }
}