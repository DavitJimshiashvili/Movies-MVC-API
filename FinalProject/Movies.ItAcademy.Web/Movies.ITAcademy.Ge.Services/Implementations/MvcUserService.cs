using MovieManagement.Data;
using Movies.ITAcademy.Ge.Services.Abstractions;
using Movies.ITAcademy.Ge.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using MovieManagement.Domain.Enums;
using MovieManagement.Domain.Enums.TicketEnums;
using MovieManagement.Domain.POCO;
using Exceptions;

namespace Movies.ITAcademy.Ge.Services.Implementations
{
    public class MVCUserService : IMvcUserService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITicketRepository _ticketRepository;

        public MVCUserService(IMovieRepository movieRepository, IUserRepository userRepository, ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
            _movieRepository = movieRepository;
            _userRepository = userRepository;
        }

        public async Task BookTicketAsync(string userId, int movieId)
        {
            if (!await _userRepository.Exists(userId) || !await _movieRepository.Exists(movieId))
            {
                throw new ObjectNotFoundException("object not found");
            }
            if (await _movieRepository.HasStatus(movieId, Statuses.Published) && !await _ticketRepository.HasStatus(userId, movieId, TKTStatuses.Purchased) && !await _ticketRepository.HasStatus(userId, movieId, TKTStatuses.Booked))
            {

                var manuallyCreatedTicket = new TicketServiceModel
                {
                    UserId = userId,
                    MovieId = movieId,
                    Status = TKTStatuses.Booked,

                };

                var ticket = manuallyCreatedTicket.Adapt<Ticket>();
                //_ticketRepository.SetAddedState(ticket);
                await _ticketRepository.CreateAsync(ticket);
                //await _ticketRepository.CreateAsync(new Ticket() { MovieId=movieId,UserId=userId, Status=TKTStatuses.Booked});

            }
            else
            {
                throw new ObjectNotFoundException("can not book ticket ");
            }

        }

        public async Task BuyTicketAsync(string userId, int movieId)
        {
            if (!await _userRepository.Exists(userId))
            {
                throw new ObjectNotFoundException("user not found");
            }

            var manuallyCreatedTicket = new TicketServiceModel()
            {
                UserId = userId,
                MovieId = movieId,
                Status = TKTStatuses.Purchased
            };
            var ticket = manuallyCreatedTicket.Adapt<Ticket>();
            if (await _ticketRepository.HasStatus(userId, movieId, TKTStatuses.Booked))
            {
                await _ticketRepository.UpdateAsync(ticket);
            }
            else
            {
                await _ticketRepository.CreateAsync(ticket);
            }

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

        public async Task<List<MovieServiceModel>> GetAvialableFilmsAsync()
        {
            var result = await _movieRepository.GetAllFilteredAsync();

            return result.Adapt<List<MovieServiceModel>>();
        }

        public async Task<MovieServiceModel> GetFilmDetails(int movieId)
        {
            if (!await _movieRepository.Exists(movieId) || !( await _movieRepository.HasStatus(movieId, Statuses.Published) || await _movieRepository.HasStatus(movieId, Statuses.Starting)))
                throw new ObjectNotFoundException("Movie Not Found");
            var result = await _movieRepository.GetAsync(movieId);
            return result.Adapt<MovieServiceModel>();

        }

        public async Task<List<int>> GetMovieIds(string userId)
        {
            return await _userRepository.GetMovieIds(userId);
        }

        public async Task<List<int>> GetMovieIds(string userId, string tktStatus)
        {
            return await _userRepository.GetMovieIds(userId, tktStatus);

        }

        public async Task<string> GetMovieSatus(int movieId)
        {
            return await _movieRepository.GetStatus(movieId);
        }

        public async Task<string> GetTicketStatus(string userId, int movieId)
        {
            return await _userRepository.GetTiketStatus(userId, movieId);
        }

    }
}

