using Microsoft.EntityFrameworkCore;
using MovieManagement.Domain.Enums;
using MovieManagement.Domain.Enums.TicketEnums;
using MovieManagement.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Data.EF.Repositories
{
    public class MovieRepository : IMovieRepository
    {

        private readonly IBaseRepository<Movie> _baseRepository;

        public MovieRepository(IBaseRepository<Movie> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<int> CreateAsync(Movie movie)
        {
            await _baseRepository.AddAsync(movie);

            return movie.Id;
        }

        public async Task DeleteAsync(int id)
        {
            await _baseRepository.RemoveAsync(id);
        }

        public async Task<bool> Exists(int id)
        {
            return await _baseRepository.AnyAsync(x => x.Id == id);
        }

        public async Task<List<Movie>> GetAllFilteredAsync()
        {
            return await (from movie in _baseRepository.Table
                          where movie.Status == Statuses.Published
                          || movie.Status == Statuses.Starting
                          select movie
                          ).ToListAsync();
        }
        public async Task<List<Movie>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync();
        }

        public async Task<List<Movie>> GetAllPendingAsync()
        {
            return await _baseRepository.Table
                .Where(x => x.Status == Statuses.Uploaded)
                .ToListAsync();
        }

        public async Task<Movie> GetAsync(int id)
        {
            return await _baseRepository.GetAsync(id);
        }

        public async Task UpdateAsync(Movie movie)
        {
            await _baseRepository.UpdateAsync(movie);
        }
        public void SetState(Movie movie, EntityState state)
        {
            _baseRepository.SetState(movie, state);
        }

        public async Task<bool> HasStatus(int movieId, string status)
        {
            return await _baseRepository.AnyAsync(x=>x.Id==movieId && x.Status==status);
        }

        public async Task<string> GetStatus(int movieId)
        {
            return await _baseRepository.Table
                .Where(x => x.Id == movieId)
                .Select(x => x.Status)
                .SingleOrDefaultAsync();
        }

        public async Task<Movie> GetNoTrackingAsync(int movieId)
        {
            return await _baseRepository.TableNoTracking
                .Where(x => x.Id == movieId)
                .SingleOrDefaultAsync();
        }
    }
}
