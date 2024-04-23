using BlogApp.Domain.Configurations;
using BlogApp.Domain.Models.Blog;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Persistence.Contexts;

public class BlogAppDbContext : DbContext
{
    public BlogAppDbContext(DbContextOptions<BlogAppDbContext> options) : base(options)
    {
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<BlogImage> BlogImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BlogConfiguration());
        modelBuilder.ApplyConfiguration(new BlogImageConfiguration());
    }
}