using MovieManagement.Domain.Enums.TicketEnums;
using MovieManagement.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.Data
{
    public interface ITicketRepository
    {
        void SetAddedState(Ticket entity);
        void SetDetachedState(Ticket entity);
        Task DeleteAsync(Ticket entity);
        Task<Ticket> GetAsync(string userId, int movieId);
        Task<List<Ticket>> GetBookedByUserId(string userId);
        Task<List<Ticket>> GetAllBookedAsync();
        Task<Tuple<string, int>> CreateAsync(Ticket entity);
        Task UpdateAsync(Ticket entity);
        Task<bool> Exists(string userId, int movieId);
        Task<bool> HasStatus(string userId, int movieId, string status);


    }
}
