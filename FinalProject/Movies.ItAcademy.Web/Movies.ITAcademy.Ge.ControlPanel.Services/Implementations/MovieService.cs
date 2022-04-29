using Exceptions;
using Mapster;
using MovieManagement.Data;
using MovieManagement.Domain.Enums;
using MovieManagement.Domain.POCO;
using Movies.ITAcademy.Ge.ControlPanel.Services.Abstractions;
using Movies.ITAcademy.Ge.ControlPanel.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movies.ITAcademy.Ge.ControlPanel.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
       // private readonly IUserRepository _userRepository;
       // private readonly ITicketRepository _ticketRepository;

        public MovieService(IMovieRepository movieRepository/*, IUserRepository userRepository, ITicketRepository ticketRepository*/)
        {
            //_ticketRepository = ticketRepository;
            _movieRepository = movieRepository;
            //_userRepository = userRepository;
        }
        public async Task<int> CreateAsync(MovieServiceModel movie)
        {
            if (await _movieRepository.Exists(movie.Id))
                throw new ObjectAlreadyExistsException("Movie already exists");


            var movieToInsert = movie.Adapt<Movie>();

            return await _movieRepository.CreateAsync(movieToInsert);
        }

        public async Task DeleteAsync(int id)
        {
            if (!await _movieRepository.Exists(id))
                throw new ObjectNotFoundException("movie doesnot exist");
            await _movieRepository.DeleteAsync(id);
        }

        public async Task<bool> Exists(int id)
        {
            return await _movieRepository.Exists(id);
        }

        public async Task<List<MovieServiceModel>> GetAllAsync()
        {
            var result = await _movieRepository.GetAllFilteredAsync();
            return result.Adapt<List<MovieServiceModel>>();
        }

        public async Task<List<MovieServiceModel>> GetAllPendingAsync()
        {
            var result = await _movieRepository.GetAllPendingAsync();
            return result.Adapt<List<MovieServiceModel>>();
        }

        public async Task<MovieServiceModel> GetAsync(int id)
        {
            if (!await _movieRepository.Exists(id))
                throw new ObjectNotFoundException("Movie not found");

            var result = await _movieRepository.GetAsync(id);
            return result.Adapt<MovieServiceModel>();
        }
        public async Task<MovieServiceModel> GetNoTrackingAsync(int id)
        {
            if (!await _movieRepository.Exists(id))
                throw new ObjectNotFoundException("Movie not found");

            var result = await _movieRepository.GetNoTrackingAsync(id);
            return result.Adapt<MovieServiceModel>();
        }
        public async Task<int> UpdateAsync(MovieServiceModel movie)
        {
            var pocoMovie=movie.Adapt<Movie>();
            await _movieRepository.UpdateAsync(pocoMovie);
            return pocoMovie.Id;
        }
    }
}
