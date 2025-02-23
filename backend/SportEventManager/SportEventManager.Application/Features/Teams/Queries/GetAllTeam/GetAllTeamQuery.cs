using MediatR;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.Common.Queries;
using SportEventManager.Application.DTOs;


namespace SportEventManager.Application.Features.Teams.Queries.GetAllTeam
{
    public record GetAllTeamQuery(PaginationFilterDto filter)
      : PaginatedQuery<TeamDto>(filter);
}
