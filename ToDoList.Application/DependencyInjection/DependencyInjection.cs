using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Mapping;
using ToDoList.Domain.Interfaces.Services;
using ToDoList.Infrastructure.Services;

namespace ToDoList.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserMapping));

            var assembly = typeof(DependencyInjection).Assembly;
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
