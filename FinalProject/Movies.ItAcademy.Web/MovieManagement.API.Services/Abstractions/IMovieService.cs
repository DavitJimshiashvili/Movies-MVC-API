using MovieManagement.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Services.Abstractions
{
    public interface IMovieService
    {
        Task<List<MovieServiceModel>> GetAllAsync();
        Task<MovieServiceModel> GetAsync(int id);
        Task<int> CreateAsync(MovieServiceModel movie);
        Task<int> UpdateAsync(MovieServiceModel movie);
        Task DeleteAsync(int id);//deletes with movieID
    }
}
