using Domain.Repository;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Configuration
{
    public static class InfrastructureIoC
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new NullReferenceException("Invalid connection string");

            services.AddDbContext<ApplicationContext>(opt => opt.UseSqlite(connectionString));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUrlRepository, UrlRepository>();
            services.AddScoped<IUrlMetricRepository, UrlMetricRepository>();
            services.BuildServiceProvider().GetService<ApplicationContext>().Database.EnsureCreated();

            return services;
        }
    }
}
