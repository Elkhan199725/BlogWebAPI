using BlogApp.Application.Repositories.Interfaces;
using BlogApp.Domain.Models.Blog;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Persistence.Repositories.Implementations;

public class BlogRepository : GenericRepository<Blog>, IBlogRepository
{
    public BlogRepository(DbContext context) : base(context)
    {
    }
}
