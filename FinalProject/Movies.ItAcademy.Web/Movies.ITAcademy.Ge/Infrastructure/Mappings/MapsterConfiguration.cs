using Mapster;
using Microsoft.Extensions.DependencyInjection;
using MovieManagement.Domain.POCO;
using Movies.ITAcademy.Ge.Models.ViewModels;
using Movies.ITAcademy.Ge.Services.Models;

namespace Movies.ITAcademy.Ge.Infrastructure.Mappings
{
    public static class MapsterConfiguration
    {
        public static void RegisterMaps(this IServiceCollection service)
        {
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

            TypeAdapterConfig<MovieCardViewModel, MovieServiceModel>
                .NewConfig()
                .TwoWays();
            TypeAdapterConfig<MovieCardViewModel, MovieDetailsViewModel>
                .NewConfig()
                .TwoWays();
            TypeAdapterConfig<MovieDetailsViewModel, MovieCardViewModel>
                .NewConfig()
                .TwoWays();

            TypeAdapterConfig<MovieServiceModel, MovieCardViewModel>
                .NewConfig()
                .TwoWays();
        }
    }
}
