using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Mapping;
using ToDoList.Application.Services;
using ToDoList.Domain.Interfaces.Services;

namespace ToDoList.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserMapping));
            services.AddAutoMapper(typeof(TaskListMapping));

            var assembly = typeof(DependencyInjection).Assembly;
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
