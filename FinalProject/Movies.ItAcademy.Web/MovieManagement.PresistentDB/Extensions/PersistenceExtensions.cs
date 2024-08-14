using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieManagement.PresistentDB.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.PresistentDB.Extensions
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MovieManagementContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}
