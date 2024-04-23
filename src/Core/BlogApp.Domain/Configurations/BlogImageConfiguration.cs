using BlogApp.Domain.Models.Blog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Configurations;

public class BlogImageConfiguration : IEntityTypeConfiguration<BlogImage>
{
    public void Configure(EntityTypeBuilder<BlogImage> builder)
    {
        builder.ToTable("BlogImages");

        builder.HasKey(bi => bi.Id);

        builder.Property(bi => bi.ImageUrl)
               .IsRequired();

        builder.HasOne(bi => bi.Blog)
               .WithMany(b => b.BlogImages)
               .HasForeignKey(bi => bi.BlogId);

        builder.Property(bi => bi.CreatedDate)
               .IsRequired();

        builder.Property(bi => bi.ModifiedDate)
               .IsRequired(false);

        builder.Property(bi => bi.ModifiedBy)
               .HasMaxLength(50)
               .IsRequired(false);
    }
}