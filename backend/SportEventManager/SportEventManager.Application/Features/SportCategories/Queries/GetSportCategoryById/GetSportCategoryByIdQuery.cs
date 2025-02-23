using MediatR;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Application.Features.SportCategories.Queries.GetSportCategoryById
{
    public class GetSportCategoryByIdQuery : IRequest<SportCategory>
    {
        public int Id { get; set; }
        public GetSportCategoryByIdQuery(int id)
        {
            Id = id;
        }
    }
}
