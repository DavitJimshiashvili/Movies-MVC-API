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
    public class UserRepository : IUserRepository
    { 
        private readonly IBaseRepository<User> _baseRepository;

        public UserRepository(IBaseRepository<User> repository)
        {
            _baseRepository = repository;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await GetOneWithData(id);
        }
        public async Task<User> GetOneWithData(string id)
        {
            return await _baseRepository.Table
                .Where(x => x.Id == id)
                .Include(x => x.Tickets)/*.ThenInclude(x => x.Movie)*/
                .SingleOrDefaultAsync();
        }

        public async Task<List<User>> GetAllWithData()
        {
            return await _baseRepository.Table
                .Include(x => x.Tickets)/*.ThenInclude(x => x.Movie)*/.ToListAsync();
        }

        public async Task<string> CreateAsync(User user)
        {
            await _baseRepository.AddAsync(user);
            return user.Id;
        }

        public async Task UpdateAsync(User user)
        {
            await _baseRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(string id)
        {
             await _baseRepository.RemoveAsync(id);
        }

        public async Task<bool> Exists(string id)
        {
            return await _baseRepository.AnyAsync(x => x.Id == id);
        }

       

        public async Task<List<int>> GetMovieIds(string id)
        {
            return await _baseRepository.Table
                .Where(x => x.Id == id)
                .SelectMany(x => x.Tickets)
                .Select(x => x.MovieId)
                .ToListAsync();
        }
        public async Task<List<int>> GetMovieIds(string id,string tktStatus)
        {
            return await _baseRepository.Table
                .Where(x => x.Id == id)
                .SelectMany(x => x.Tickets)
                .Where(x=>x.Status==tktStatus)
                .Select(x => x.MovieId)
                .ToListAsync();
        }
        public async Task<List<Movie>> GetUserMovies(string userId)
        {
            return await _baseRepository.Table
                .Where(x => x.Id == userId)
                .SelectMany(x => x.Tickets)
                .Where(x=>x.Status==TKTStatuses.Purchased)
                .Select(x => x.Movie)
                .Where(x=>x.Status==Statuses.Published)
                .ToListAsync();
        }

        public async Task<User> GetByUserNameAndPassword(string username, string password)
        {
            return await _baseRepository.Table.SingleOrDefaultAsync(x => x.UserName == username && x.Password == password);
        }

        public void SetDetachedState(User user)
        {
            _baseRepository.SetState(user, EntityState.Detached);
        }

        public async Task<string> GetTiketStatus(string userId, int movieId)
        {
            return await _baseRepository.Table
                 .Where(x => x.Id == userId)
                 .SelectMany(x => x.Tickets)
                 .Where(x => x.MovieId == movieId)
                 .Select(x => x.Status).SingleOrDefaultAsync();
        }

        public async Task<string> GetUserId(string username)
        {
            return await _baseRepository.Table
                .Where(x => x.UserName == username)
                .Select(x => x.Id)
                .SingleOrDefaultAsync();
        }

        public async Task<bool> ExistsUsername(string username)
        {
            return await _baseRepository.AnyAsync(x => x.UserName == username);
        }
    }
}
