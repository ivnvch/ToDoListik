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
            //var connectionString = configuration.GetConnectionString("PostgresSql");
            var connectionString = configuration.GetConnectionString("MSSQL");

            services.AddDbContext<DataContext>(options =>
            {
                //options.UseNpgsql(connectionString);
                options.UseSqlServer(connectionString);
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
