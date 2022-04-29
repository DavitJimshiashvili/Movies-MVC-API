using MovieManagement.Data;
using MovieManagement.Domain.Enums;
using MovieManagement.PresistentDB.Context;

namespace MyWorkerService
{
    public class ScopedMovieService : IScopedMovieService
    {
        private readonly IServiceScopeFactory _services;

        public ScopedMovieService(IServiceScopeFactory services)
        {
            _services = services;

        }
        public async Task UpadateMovieStatus()
        {
            using (var scope = _services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MovieManagementContext>();
                var _repo = scope.ServiceProvider.GetRequiredService<IMovieRepository>();

                foreach (var movie in await _repo.GetAllAsync())
                {
                    var timenow = DateTime.Now;
                    if (movie.EndTime < timenow)
                    {
                        movie.Status = Statuses.Archived;
                        await _repo.UpdateAsync(movie);
                    }
                    if (movie.StartTime.AddHours(-1)<=timenow && timenow<movie.StartTime )
                    {
                        movie.Status = Statuses.Starting;
                        await _repo.UpdateAsync(movie);
                    }
                    if (movie.StartTime < timenow && timenow < movie.EndTime)
                    {
                        movie.Status = Statuses.Ongoing;
                        await _repo.UpdateAsync(movie);
                    }
                }
            }

        }

    }
}
