using Microsoft.Extensions.DependencyInjection;
using MovieManagement.Data;
using MovieManagement.Data.EF;
using MovieManagement.Data.EF.Repositories;

namespace Movies.ItAcademy.API.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        }
    }
}
