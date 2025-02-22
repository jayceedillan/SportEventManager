using MediatR;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Application.Features.SportCategories.Commands.UpdateSportCategory
{
    public class UpdateSportCategoryCommand : IRequest<SportCategory>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public int? ParentId { get; set; }
    }
}
