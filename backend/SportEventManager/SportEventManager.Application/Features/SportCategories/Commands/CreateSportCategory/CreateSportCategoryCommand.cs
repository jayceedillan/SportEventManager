using MediatR;
using SportEventManager.Application.DTOs;

namespace SportEventManager.Application.Features.SportCategories.Commands.CreateSportCategory
{
    public class CreateSportCategoryCommand : IRequest<SportCategoryDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
    }
}
