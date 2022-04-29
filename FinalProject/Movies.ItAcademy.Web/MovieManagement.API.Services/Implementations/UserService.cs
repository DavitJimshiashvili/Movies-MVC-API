using Exceptions;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using MovieManagement.Data;
using MovieManagement.Domain.Enums;
using MovieManagement.Domain.Enums.TicketEnums;
using MovieManagement.Domain.POCO;
using MovieManagement.Services.Abstractions;
using MovieManagement.Services.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MovieManagement.Services.Implementations
{

    [Authorize]
    public class UserService : IUserService
    {

        private readonly IUserRepository  _userRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IJWTService _jwtService;

        public UserService(IUserRepository userRepository, ITicketRepository ticketRepository, IMovieRepository movieRepository, IJWTService jwtService)
        {
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
            _movieRepository = movieRepository;
            _jwtService = jwtService;
        }

        public async Task<string> AuthenticateAsync(string username, string password)//for login
        {
            var userEntity = await _userRepository.GetByUserNameAndPassword(username, password);
            if (userEntity == null)
                throw new ObjectNotFoundException("user not found");


            return _jwtService.GenerateJWT(userEntity.Id);
        }


        [AllowAnonymous]
        public async Task<string> RegisterAsync(UserServiceModel user)// for register
        {
            if (await _userRepository.Exists(user.Id) || await _userRepository.ExistsUsername(user.UserName))
                throw new ObjectAlreadyExistsException("user already exists");


            var userToinsert = user.Adapt<User>();
            userToinsert.EmailConfirmed = true;
            userToinsert.NormalizedEmail = userToinsert.Email.ToUpper();
            userToinsert.NormalizedUserName = userToinsert.UserName.ToUpper();
            userToinsert.PasswordHash = HashPassword(userToinsert.Password);

            return await _userRepository.CreateAsync(userToinsert);
        }

        public async Task<List<MovieServiceModel>> GetAvialableFilmsAsync()
        {
            var result = await _movieRepository.GetAllFilteredAsync();
            return result.Adapt<List<MovieServiceModel>>();
        }

        public async Task<int> BookTicketAsync(string userId, int movieId)
        {
            
            if ( !await _movieRepository.Exists(movieId)||!await _userRepository.Exists(userId))
            {
                throw new ObjectNotFoundException("object not found");
            }
            if (await _movieRepository.HasStatus(movieId, Statuses.Published) && !await _ticketRepository.HasStatus(userId, movieId, TKTStatuses.Purchased) && !await _ticketRepository.HasStatus(userId, movieId, TKTStatuses.Booked))
            {

                var manuallyCreatedTkt = new TicketServiceModel()
                {
                    UserId = userId,
                    MovieId = movieId,
                    Status = TKTStatuses.Booked
                };
                var ticket = manuallyCreatedTkt.Adapt<Ticket>();
                await _ticketRepository.CreateAsync(ticket);

                return movieId;

            }
            throw new ObjectNotFoundException("can not book ticket ");

        }
        public async Task<int> BuyTicketAsync(string userId, int movieId)
        {
            if (!await _userRepository.Exists(userId) || !await _movieRepository.Exists(movieId))//meore sheidzleba dakomentardes
            {
                throw new ObjectNotFoundException("user not found");
            }
            if (!await _ticketRepository.HasStatus(userId, movieId, TKTStatuses.Purchased) && await _movieRepository.HasStatus(movieId, Statuses.Published) && await _ticketRepository.HasStatus(userId, movieId, TKTStatuses.Booked))
            {
                var manuallyCreatedTicket = new TicketServiceModel()
                {
                    UserId = userId,
                    MovieId = movieId,
                    Status = TKTStatuses.Purchased
                };
                var ticket = manuallyCreatedTicket.Adapt<Ticket>();
                await _ticketRepository.UpdateAsync(ticket);

                return movieId;

            }
            throw new ObjectNotFoundException("Can not buy ticket");


        }
        public async Task<int> CancelTicketAsync(string userId, int movieId)
        {
            if (!await _userRepository.Exists(userId))
            {
                throw new ObjectNotFoundException("user not found");
            }

            if (await _ticketRepository.HasStatus(userId, movieId, TKTStatuses.Booked))
            {
                await _ticketRepository.DeleteAsync(new TicketServiceModel() { MovieId = movieId, UserId = userId }.Adapt<Ticket>());

                return movieId;
            }
            throw new ObjectNotFoundException("can not cancel ticket");

            #region OldCode
            //var manuallyCreatedTicket = new TicketServiceModel()
            //{
            //    UserId = userId,
            //    MovieId = movieId,
            //    Status = Statuses.Purchased
            //};
            //var ticket = manuallyCreatedTicket.Adapt<Ticket>();

            //_userRepository.SetDetachedState(user);// entity trackavs iuzeris obieqts da tu steiti ar 
            //                                       //shevucvale mashin ver daaafdeitebs radgan ori sxvadasxva metodi mimartavs
            //                                       //am shemtxvevashi notrackingit wamogeba ar gamogvadgeba radgan user icvleba
            //                                       //da useris biletebis raodenoba xdeba 1

            //await _ticketRepository.UpdateAsync(ticket);
            #endregion

        }
        public async Task<List<MovieServiceModel>> GetUserFilms(string userId)
        {
            if (!await _userRepository.Exists(userId))
            {
                throw new ObjectNotFoundException("user not found");
            }
            var result = await _userRepository.GetUserMovies(userId);
            return result.Adapt<List<MovieServiceModel>>();


        }

        private static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

    }
}
