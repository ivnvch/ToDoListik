﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoList.Persistence.DependencyInjection
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddDataBaseExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresSql");

            services.AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.RegisterRepository();

            return services;
        }

        private static void RegisterRepository(this IServiceCollection services)
        {

        }
    }
}