using SportEventManager.Application.Common.Models;
using SportEventManager.Application.Common.Queries;
using SportEventManager.Application.DTOs;


namespace SportEventManager.Application.Features.Users.Queries.GetAllUser
{
    public record GetAllUserQuery(PaginationFilterDto filter)
       : PaginatedQuery<UserDto>(filter);
}
