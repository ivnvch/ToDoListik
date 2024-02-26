using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Domain.Interfaces.Repositories;
using ToDoList.Persistence.Repositories;

namespace ToDoList.Persistence.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
