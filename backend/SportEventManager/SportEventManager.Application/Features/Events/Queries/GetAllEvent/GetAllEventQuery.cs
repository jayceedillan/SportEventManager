using MediatR;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.DTOs;


namespace SportEventManager.Application.Features.Events.Queries.GetAllEvent
{
    public record GetAllEventQuery : IRequest<PaginatedResult<EventDto>>
    {
        public string? SearchTerm { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public string? SortBy { get; init; }
        public bool SortDescending { get; init; }

        public GetAllEventQuery(PaginationFilterDto filter)
        {
            SearchTerm = filter.SearchTerm;
            PageNumber = filter.PageNumber;
            PageSize = filter.PageSize;
            SortBy = filter.SortBy;
            SortDescending = filter.SortDescending;
        }
    }
}
