using MediatR;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Application.Features.Players.Queries.GetPlayerByIdQuery
{
    public class GetPlayerByIdQuery : IRequest<Player>
    {
        public int Id { get; set; }

        public GetPlayerByIdQuery(int id)
        {
            Id = id;
        }
    }
}
