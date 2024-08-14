using Microsoft.Extensions.DependencyInjection;
using Movies.ITAcademy.Ge.Services.Abstractions;
using Movies.ITAcademy.Ge.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.ITAcademy.Ge.Services.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterMVCService(this IServiceCollection services)
        {
            services.AddScoped<IMvcUserService, MVCUserService>();
        }
    }
}
