using AutoMapper;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.Features.SportCategories.DTOs;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SportCategory, SportCategoryDto>().ReverseMap();
            //CreateMap<CreateSportCategoryCommand, SportCategory>();
            CreateMap<List<SportCategoryDto>, PaginatedResult<SportCategoryDto>>()
           .ConvertUsing(new ListToPaginatedResultConverter<SportCategoryDto>());
        }
    }
}
