using System;
using AspNet.Security.OpenIdConnect.Primitives;
using BlogEngine.Data.Entities;
using BlogEngine.Data.Interface;
using BlogEngine.Data.Interfaces;
using BlogEngine.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;

namespace BlogEngine.Data
{
    public static class DataConfiguration
    {
        public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            // Entity framework configuration
            services.AddDbContext<BlogEngineContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.UseOpenIddict();
            });

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<BlogEngineContext>()
                .AddDefaultTokenProviders();
            services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore().UseDbContext<BlogEngineContext>();
                });
            services.AddScoped<IDbContext, BlogEngineContext>();
            services.AddScoped<IUnitOfWork<Guid>, UnitOfWork<Guid>>();
            services.AddScoped<IBlogRepository, BlogRepository>();
        }
    }
}
