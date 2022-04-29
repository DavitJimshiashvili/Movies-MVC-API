using Mapster;
using Microsoft.Extensions.DependencyInjection;
using MovieManagement.Domain.POCO;
using Movies.ITAcademy.Ge.ControlPanel.Models.ViewModels;
using Movies.ITAcademy.Ge.ControlPanel.Services.Models;

namespace Movies.ITAcademy.Ge.ControlPanel.Infrastructure
{
    public static class MapsterConfiguration
    {
        public static void RegisterMaps(this IServiceCollection service)
        {
            

            TypeAdapterConfig<Ticket, TicketServiceModel>.NewConfig().TwoWays();
            TypeAdapterConfig<TicketServiceModel, Ticket>.NewConfig().TwoWays();
            TypeAdapterConfig<TicketServiceModel, TicketViewModel>.NewConfig().TwoWays();
            TypeAdapterConfig<TicketViewModel, TicketServiceModel>.NewConfig().TwoWays();

            TypeAdapterConfig<UserServiceModel, UserViewModel>.NewConfig().TwoWays();
            TypeAdapterConfig<UserViewModel, UserServiceModel>.NewConfig().TwoWays();


            TypeAdapterConfig<MovieServiceModel, Movie>.NewConfig().TwoWays();
            TypeAdapterConfig<Movie, MovieServiceModel>.NewConfig().TwoWays();
            TypeAdapterConfig<MovieCardViewModel, MovieServiceModel>.NewConfig().TwoWays();
            TypeAdapterConfig<MovieServiceModel, MovieCardViewModel>.NewConfig().TwoWays();
            TypeAdapterConfig<MovieCreateViewModel, MovieServiceModel>.NewConfig().TwoWays();
            TypeAdapterConfig<MovieServiceModel, MovieCreateViewModel>.NewConfig().TwoWays();

        }
    }
}
