using MovieManagement.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Services.Abstractions
{
    public interface IUserService
    {
        Task<List<MovieServiceModel>> GetAvialableFilmsAsync();
        Task<List<MovieServiceModel>> GetUserFilms(string  userId);
        Task<int> BuyTicketAsync(string userId, int movieId);
        Task<int> CancelTicketAsync(string userId, int movieId);
        Task<int> BookTicketAsync(string userId,int movieId);
        Task<string> RegisterAsync(UserServiceModel user);
        Task<string> AuthenticateAsync(string userName, string password);
    }
}
