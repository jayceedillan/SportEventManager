using MediatR;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.Common.Queries;
using SportEventManager.Application.DTOs;


namespace SportEventManager.Application.Features.Events.Queries.GetAllEvent
{
    public record GetAllEventQuery(PaginationFilterDto filter)
      : PaginatedQuery<EventDto>(filter);
}
