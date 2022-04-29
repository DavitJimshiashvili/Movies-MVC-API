using Movies.ITAcademy.Ge.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movies.ITAcademy.Ge.Services.Abstractions
{
    public interface IMvcUserService
    {
        Task<List<MovieServiceModel>> GetAvialableFilmsAsync();
        Task<MovieServiceModel> GetFilmDetails(int movieId);
        Task<string> GetTicketStatus(string userId, int movieId);
        Task<List<int>> GetMovieIds(string userId);
        Task<List<int>> GetMovieIds(string userId, string tktStatus);
        Task<string> GetMovieSatus(int movieId);
        Task BookTicketAsync(string userId, int movieId);
        Task BuyTicketAsync(string userId, int movieId);
        Task CancelTicketAsync(string userId, int movieId);
    }
}
