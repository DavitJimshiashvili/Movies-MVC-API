using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MovieManagement.Domain.POCO;
using MovieManagement.Services.Models;
using Movies.ItAcademy.API.Models.DTOs;
using Movies.ItAcademy.API.Models.Requests.Account;
using System.Linq;

namespace Movies.ItAcademy.API.Infrastructure.Mappings
{
    public static class MapsterConfiguration
    {
        public static void RegisterMaps(this IServiceCollection service)
        {

            TypeAdapterConfig<AccountRegisterRequest, UserServiceModel>
               .NewConfig()
               .TwoWays();

            //TypeAdapterConfig<AccountLogInRequest, UserServiceModel>
            //.NewConfig()
            //.TwoWays();

            TypeAdapterConfig<UserDTO, UserServiceModel>
                .NewConfig()
                .TwoWays();

            TypeAdapterConfig<UserServiceModel, UserDTO>
                .NewConfig()
                .TwoWays();


            TypeAdapterConfig<UserServiceModel, User>
                .NewConfig()
                .Map(dest => dest.Tickets, src => src.Tickets != null ? src.Tickets
                 .Select(x => new Ticket
                 {
                     Movie = x.Adapt<Movie>(),
                     MovieId = x.MovieId
                 }) : default);


            TypeAdapterConfig<User, UserServiceModel>
                .NewConfig()
                .Map(dest => dest.Tickets, src => src.Tickets
                .Select(x => x.Movie));

            TypeAdapterConfig<MovieDTO, MovieServiceModel>
                .NewConfig()
                .TwoWays();

            TypeAdapterConfig<MovieServiceModel, MovieDTO>
                .NewConfig()
                .TwoWays();


            TypeAdapterConfig<MovieServiceModel, Movie>
                .NewConfig()
                .TwoWays();
            TypeAdapterConfig<Movie, MovieServiceModel>
                .NewConfig()
                .TwoWays();

            TypeAdapterConfig<Ticket, TicketServiceModel>
              .NewConfig()
              .TwoWays();

            TypeAdapterConfig<TicketServiceModel, Ticket>
                .NewConfig()
                .TwoWays();

        }

    }

}
