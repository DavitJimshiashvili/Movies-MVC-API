using Microsoft.Extensions.DependencyInjection;
using Movies.ITAcademy.Ge.ControlPanel.Services.Abstractions;
using Movies.ITAcademy.Ge.ControlPanel.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.ITAcademy.Ge.ControlPanel.Services.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddAppService(this IServiceCollection services)
        {
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IUserSevice, UserService>();
        }
    }
}
