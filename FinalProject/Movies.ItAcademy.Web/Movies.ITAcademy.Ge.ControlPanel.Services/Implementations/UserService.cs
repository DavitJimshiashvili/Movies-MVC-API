using Exceptions;
using Mapster;
using MovieManagement.Data;
using MovieManagement.Domain.Enums.TicketEnums;
using MovieManagement.Domain.POCO;
using Movies.ITAcademy.Ge.ControlPanel.Services.Abstractions;
using Movies.ITAcademy.Ge.ControlPanel.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movies.ITAcademy.Ge.ControlPanel.Services.Implementations
{
    public class UserService : IUserSevice
    {

        private readonly IUserRepository _userRepository;
        private readonly ITicketRepository _ticketRepository;

        public UserService(IUserRepository userRepository, ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
        }
        public async Task<string> CreateAsync(UserServiceModel user)
        {
            //if (await _userRepository.Exists(user.Id))
            //    throw new ObjectAlreadyExistsException("user already exists");

            var userToInsert = user.Adapt<User>();

            return await _userRepository.CreateAsync(userToInsert);
        }

        public async Task DeleteAsync(string id)
        {
            if (!await _userRepository.Exists(id))
                throw new ObjectNotFoundException("User Not Found");
            await _userRepository.DeleteAsync(id);
            
        }

        public async Task<bool> Exists(string id)
        {
            return await _userRepository.Exists(id);
        }

        public async Task<List<UserServiceModel>> GetAllAsync()
        {
            var pocoUsers = await _userRepository.GetAllAsync();
            return pocoUsers.Adapt<List<UserServiceModel>>();
        }

        public async Task<UserServiceModel> GetAsync(string id)
        {
            var user=await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new ObjectNotFoundException("user not found");
            return user.Adapt<UserServiceModel>();
            
        }

        public async Task UpdateAsync(UserServiceModel user)
        {
            var pocoUser = user.Adapt<User>();
            await _userRepository.UpdateAsync(pocoUser);
        }
        public async Task CancelTicketAsync(string userId, int movieId)
        {
            if (!await _userRepository.Exists(userId))
            {
                throw new ObjectNotFoundException("user not found");
            }

            if (await _ticketRepository.HasStatus(userId, movieId, TKTStatuses.Booked))
            {
                await _ticketRepository.DeleteAsync(new TicketServiceModel() { MovieId = movieId, UserId = userId }.Adapt<Ticket>());

            }
            else
            {
                throw new ObjectNotFoundException("can not cancel ticket");
            }
        }

        public async Task<List<TicketServiceModel>> GetUserTickets(string userId)
        {
            if (!await _userRepository.Exists(userId))
                throw new ObjectNotFoundException("User Not Found");

            var tickets=await _ticketRepository.GetBookedByUserId(userId);

            return tickets.Adapt<List<TicketServiceModel>>();

        }

        public async Task<List<TicketServiceModel>> GetAllBookedTickets()
        {
            var tickets = await _ticketRepository.GetAllBookedAsync();

            return tickets.Adapt<List<TicketServiceModel>>();
        }
    }
}
