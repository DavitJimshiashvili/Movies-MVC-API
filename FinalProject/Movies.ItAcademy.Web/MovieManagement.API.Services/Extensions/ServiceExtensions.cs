using Microsoft.Extensions.DependencyInjection;
using MovieManagement.Services.Abstractions;
using MovieManagement.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.API.Services.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddAppService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJWTService, JWTService>();
        }
    }
}
