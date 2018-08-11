using AutoMapper;
using BlogEngine.Data;
using BlogEngine.Data.Entities;
using BlogEngine.Service.Dtos;
using BlogEngine.Service.Interfaces;
using BlogEngine.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngine.Utility
{
    public class SeviceConfiguration
    {
        public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            DataConfiguration.ConfigureService(services, configuration);
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IAccountService, AccountService>();
        }
    }
    public class Mapper: Profile
    {
        public Mapper()
        {
            CreateMap<Blog, BlogDTO>();
        }
    }
}
