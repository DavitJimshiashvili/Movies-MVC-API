using Microsoft.EntityFrameworkCore;
using MovieManagement.Domain.Enums.TicketEnums;
using MovieManagement.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Data.EF.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IBaseRepository<Ticket> _baseRepository;

        public TicketRepository(IBaseRepository<Ticket> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<Tuple<string, int>> CreateAsync(Ticket entity)
        {
            await _baseRepository.AddAsync(entity);
            return new Tuple<string, int>(entity.UserId, entity.MovieId);
        }

        public async Task DeleteAsync(Ticket entity)
        {
            await _baseRepository.RemoveAsync(entity);
        }

        public async Task<bool> Exists(string userId, int movieId)
        {
            return await _baseRepository.AnyAsync(x => x.UserId == userId && x.MovieId == movieId);
        }

        public async Task<Ticket> GetAsync(string userId, int movieId)
        {
            return await _baseRepository.Table
                .SingleOrDefaultAsync(x => x.UserId == userId && x.MovieId == movieId);
        }

        public async Task<List<Ticket>> GetBookedByUserId(string userId)//booked tickets
        {
            return await _baseRepository.Table
                 .Where(x => x.UserId == userId && x.Status==TKTStatuses.Booked)
                 .ToListAsync();
        }

        public async Task<List<Ticket>> GetAllBookedAsync()
        {
            return await _baseRepository.Table
                .Where(x => x.Status == TKTStatuses.Booked)
                .ToListAsync();
        }

        public async Task<bool> HasStatus(string userId, int movieId, string status)
        {
            return await _baseRepository.AnyAsync(x => x.UserId == userId && x.MovieId == movieId && x.Status == status);
        }

        public void SetAddedState(Ticket entity)
        {
            _baseRepository.SetState(entity, EntityState.Added);
        }
        public void SetDetachedState(Ticket entity)
        {
            _baseRepository.SetState(entity, EntityState.Detached);
        }

        public async Task UpdateAsync(Ticket entity)
        {
            await _baseRepository.UpdateAsync(entity);
        }
    }
}
