using MediatR;
using SportEventManager.Application.DTOs;

namespace SportEventManager.Application.Features.Players.Queries.GetPlayerByIdQuery
{
    public class GetPlayerByIdQuery : IRequest<PlayerDto>
    {
        public int Id { get; set; }

        public GetPlayerByIdQuery(int id)
        {
            Id = id;
        }
    }
}
