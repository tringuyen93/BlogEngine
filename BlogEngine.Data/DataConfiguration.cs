using System;
using System.Collections.Generic;
using System.Text;
using BlogEngine.Data.Entities;
using BlogEngine.Data.Interface;
using BlogEngine.Data.Interfaces;
using BlogEngine.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngine.Data
{
    public static class DataConfiguration
    {
        public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            // Entity framework configuration
            services.AddDbContext<BlogEngineContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IDbContext, BlogEngineContext>();
            services.AddScoped<IUnitOfWork<Guid>, UnitOfWork<Guid>>();
            services.AddSingleton<IBlogRepository, BlogRepository>();
        }
    }
}
