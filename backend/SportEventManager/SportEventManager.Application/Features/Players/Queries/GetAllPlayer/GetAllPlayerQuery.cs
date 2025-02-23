using SportEventManager.Application.Common.Models;
using SportEventManager.Application.Common.Queries;
using SportEventManager.Application.DTOs;


namespace SportEventManager.Application.Features.Players.Queries.GetAllPlayer
{
    public record GetAllPlayerQuery(PaginationFilterDto filter)
       : PaginatedQuery<PlayerDto>(filter);
}
