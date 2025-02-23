using AutoMapper;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.Features.SportCategories.Commands.CreateSportCategory;
using SportEventManager.Application.Features.SportCategories.Commands.UpdateSportCategory;
using SportEventManager.Application.Features.SportCategories.DTOs;
using SportEventManager.Application.Features.Sports.Commands.CreateSport;
using SportEventManager.Application.Features.Sports.Commands.UpdateSport;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SportCategory, SportCategoryDto>().ReverseMap();
            CreateMap<CreateSportCategoryCommand, SportCategory>().ReverseMap();
            CreateMap<List<SportCategoryDto>, PaginatedResult<SportCategoryDto>>()
           .ConvertUsing(new ListToPaginatedResultConverter<SportCategoryDto>());
            CreateMap<UpdateSportCategoryCommand, SportCategory>().ReverseMap();
            CreateMap<Sport, SportDto>().ReverseMap();
            CreateMap<CreateSportCommand, Sport>();
            CreateMap<UpdateSportCommand, Sport>();
        }
    }
}
