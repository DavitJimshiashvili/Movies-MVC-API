using Microsoft.Extensions.DependencyInjection;
using MovieManagement.Services.Abstractions;
using MovieManagement.Services.Implementations;

namespace Movies.ItAcademy.API.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJWTService, JWTService>();

            services.AddRepositories();

        }
    }
}
