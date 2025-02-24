using MediatR;
using SportEventManager.Application.DTOs;

namespace SportEventManager.Application.Features.SportCategories.Queries.GetSportCategoryById
{
    public class GetSportCategoryByIdQuery : IRequest<SportCategoryDto>
    {
        public int Id { get; set; }
        public GetSportCategoryByIdQuery(int id)
        {
            Id = id;
        }
    }
}
