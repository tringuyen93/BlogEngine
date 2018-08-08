using BlogEngine.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngine.Utility
{
    public class SeviceConfiguration
    {
        public static void ConfigureService(IServiceCollection services, IConfigurationRoot configuration)
        {
            DataConfiguration.ConfigureService(services, configuration);
        }
    }
}
