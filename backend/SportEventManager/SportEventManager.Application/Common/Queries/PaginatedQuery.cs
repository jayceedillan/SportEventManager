using MediatR;
using SportEventManager.Application.Common.Models;

namespace SportEventManager.Application.Common.Queries
{
    public abstract record PaginatedQuery<T> : IRequest<PaginatedResult<T>>
    {
        public string? SearchTerm { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public string? SortBy { get; init; }
        public bool SortDescending { get; init; }

        protected PaginatedQuery(PaginationFilterDto filter)
        {
            SearchTerm = filter.SearchTerm;
            PageNumber = filter.PageNumber;
            PageSize = filter.PageSize;
            SortBy = filter.SortBy;
            SortDescending = filter.SortDescending;
        }
    }
}
