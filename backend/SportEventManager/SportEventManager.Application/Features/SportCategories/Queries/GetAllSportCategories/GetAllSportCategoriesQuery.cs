using MediatR;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.Common.Queries;
using SportEventManager.Application.DTOs;

namespace SportEventManager.Application.Features.SportCategories.Queries.GetAllSportCategories
{
    public record GetAllSportCategoriesQuery(PaginationFilterDto filter)
       : PaginatedQuery<SportCategoryDto>(filter);
}
