using Application.Services;
using Domain.Services;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration
{
    public static class ApplicationIoC 
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUrlService, UrlService>();

            var connectionString = configuration.GetConnectionString("Default");
            services.AddInfrastructure(connectionString);

            return services;
        }
    }
}
