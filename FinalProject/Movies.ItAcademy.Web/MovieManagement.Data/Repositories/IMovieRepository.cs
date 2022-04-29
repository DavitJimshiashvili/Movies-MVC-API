using Microsoft.EntityFrameworkCore;
using MovieManagement.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Data
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAllFilteredAsync();
        Task<List<Movie>> GetAllAsync();
        Task<List<Movie>> GetAllPendingAsync();
        ////Task<List<Movie>> GetUserMovies(string username);
        Task<Movie> GetAsync(int movieId);
        Task<Movie> GetNoTrackingAsync(int movieId);
        Task<int> CreateAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(int movieId);
        Task<bool> Exists(int movieId);
        Task<bool> HasStatus(int movieId, string status);
        Task<string> GetStatus(int movieId);
        void SetState(Movie movie, EntityState state);
        
    }
}
