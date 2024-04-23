using BlogApp.Application.Repositories.Interfaces;
using BlogApp.Persistence.Contexts;
using BlogApp.Persistence.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Persistence;

public static class PersistenceRegistration
{
    
    public static void AddPersistenceServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<BlogAppDbContext>
            (option => option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        service.AddScoped<IBlogRepository, BlogRepository>();
    }
}
