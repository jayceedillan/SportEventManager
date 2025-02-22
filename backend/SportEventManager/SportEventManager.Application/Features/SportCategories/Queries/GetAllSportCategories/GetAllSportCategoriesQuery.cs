using MediatR;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Application.Features.SportCategories.Queries.GetAllSportCategories
{
    public class GetAllSportCategoriesQuery : IRequest<IReadOnlyList<SportCategory>>
    {
    }
}
