using MediatR;
using SportEventManager.Application.Features.SportCategories.DTOs;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Application.Features.SportCategories.Commands.CreateSportCategory
{
    public class CreateSportCategoryCommand : IRequest<SportCategoryDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public int? ParentId { get; set; }
    }
}
