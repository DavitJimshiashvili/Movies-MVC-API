using Microsoft.Extensions.DependencyInjection;
using MovieManagement.Data.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Data.EF
{
    public static class RepositoryExtensions
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
    }
}
