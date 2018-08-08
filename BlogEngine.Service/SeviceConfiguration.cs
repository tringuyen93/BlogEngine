using AutoMapper;
using BlogEngine.Data;
using BlogEngine.Data.Entities;
using BlogEngine.Service.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngine.Utility
{
    public class SeviceConfiguration
    {
        public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            DataConfiguration.ConfigureService(services, configuration);
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
