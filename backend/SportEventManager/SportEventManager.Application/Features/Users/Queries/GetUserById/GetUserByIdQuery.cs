using MediatR;
using SportEventManager.Application.DTOs;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Application.Features.Users.Queries.GetUserByIdQuery
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public string Id { get; set; }

        public GetUserByIdQuery(string id)
        {
            Id = id;
        }
    }
}
