using MediatR;
using SportEventManager.Application.DTOs;

namespace SportEventManager.Application.Features.SportCategories.Commands.UpdateSportCategory
{
    public class UpdateSportCategoryCommand : IRequest<SportCategoryDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
    }
}
