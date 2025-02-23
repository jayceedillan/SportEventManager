using MediatR;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.Common.Queries;
using SportEventManager.Application.DTOs;

namespace SportEventManager.Application.Features.Sports.Queries.GetAllSport
{
    public record GetAllSportQuery(PaginationFilterDto filter)
       : PaginatedQuery<SportDto>(filter);
}
