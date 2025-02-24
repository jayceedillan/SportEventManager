using AutoMapper;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.DTOs;
using SportEventManager.Application.Features.Events.Commands.CreateEvent;
using SportEventManager.Application.Features.Events.Commands.UpdateEvent;
using SportEventManager.Application.Features.Players.Commands.CreatePlayer;
using SportEventManager.Application.Features.Players.Commands.UpdatePlayer;
using SportEventManager.Application.Features.SportCategories.Commands.CreateSportCategory;
using SportEventManager.Application.Features.SportCategories.Commands.UpdateSportCategory;
using SportEventManager.Application.Features.Sports.Commands.CreateSport;
using SportEventManager.Application.Features.Sports.Commands.UpdateSport;
using SportEventManager.Application.Features.Teams.Commands.CreateTeam;
using SportEventManager.Application.Features.Teams.Commands.UpdateTeam;
using SportEventManager.Application.Features.Users.Commands.CreateUser;
using SportEventManager.Application.Features.Users.Commands.UpdateUser;
using SportEventManager.Application.Features.Venues.Commands.CreateEvent;
using SportEventManager.Application.Features.Venues.Commands.UpdateEvent;
using SportEventManager.Domain.Entities;


namespace SportEventManager.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SportCategory, SportCategoryDto>().ReverseMap();
            CreateMap<CreateSportCategoryCommand, SportCategory>();
            CreateMap<UpdateSportCategoryCommand, SportCategory>();
            CreateMap<List<SportCategoryDto>, PaginatedResult<SportCategoryDto>>()
           .ConvertUsing(new ListToPaginatedResultConverter<SportCategoryDto>());
            CreateMap<Sport, SportDto>().ReverseMap();
            CreateMap<CreateSportCommand, Sport>();
            CreateMap<UpdateSportCommand, Sport>();
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<CreateEventCommand, Event>();
            CreateMap<UpdateEventCommand, Event>();

            CreateMap<Venue, VenueDto>().ReverseMap();
            CreateMap<CreateVenueCommand, Venue>();
            CreateMap<UpdateVenueCommand, Venue>();

            CreateMap<Team, TeamDto>().ReverseMap();
            CreateMap<CreateTeamCommand, Team>();
            CreateMap<UpdateTeamCommand, Team>();

            CreateMap<Player, PlayerDto>().ReverseMap();
            CreateMap<CreatePlayerCommand, Player>();
            CreateMap<UpdatePlayerCommand, Player>();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<CreateUserCommand, User>();
            CreateMap<UpdateUserCommand, User>();
        }
    }
}
