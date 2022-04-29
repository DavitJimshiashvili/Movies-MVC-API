using MovieManagement.Domain.POCO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieManagement.Data
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();//get all users
        Task<List<User>> GetAllWithData();//get all users with their data(booked or purchased movies)
        Task<List<Movie>> GetUserMovies(string userId);
        Task<string> GetTiketStatus(string userId, int movieId);
        Task<User> GetByIdAsync(string id);//get one user by ID
        Task<User> GetByUserNameAndPassword(string username, string password);//get user by password and name for authentication
        Task<string> CreateAsync(User user);//returns created user's ID
        Task UpdateAsync(User user);
        Task DeleteAsync(string id);
        Task<bool> Exists(string id);//returns true or false
        Task<bool> ExistsUsername(string username);//returns true or false
        Task<List<int>> GetMovieIds(string id);
        Task<List<int>> GetMovieIds(string id, string tktStatus);
        void SetDetachedState(User user);
        Task<string> GetUserId(string username);

    }
}
