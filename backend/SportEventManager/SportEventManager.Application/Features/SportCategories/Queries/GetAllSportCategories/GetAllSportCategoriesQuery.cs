using MediatR;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.Features.SportCategories.DTOs;

namespace SportEventManager.Application.Features.SportCategories.Queries.GetAllSportCategories
{
    public record GetAllSportCategoriesQuery : IRequest<PaginatedResult<SportCategoryDto>>
    {
        public string? SearchTerm { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public string? SortBy { get; init; }
        public bool SortDescending { get; init; }

        public GetAllSportCategoriesQuery(PaginationFilterDto filter)
        {
            SearchTerm = filter.SearchTerm;
            PageNumber = filter.PageNumber;
            PageSize = filter.PageSize;
            SortBy = filter.SortBy;
            SortDescending = filter.SortDescending;
        }
    }
}
