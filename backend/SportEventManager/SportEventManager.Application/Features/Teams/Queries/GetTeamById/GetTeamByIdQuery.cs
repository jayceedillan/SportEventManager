using MediatR;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Application.Features.Teams.Queries.GetTeamById
{
    public class GetTeamByIdQuery : IRequest<Team>
    {
        public int Id { get; set; }

        public GetTeamByIdQuery(int id)
        {
            Id = id;
        }
    }
}
