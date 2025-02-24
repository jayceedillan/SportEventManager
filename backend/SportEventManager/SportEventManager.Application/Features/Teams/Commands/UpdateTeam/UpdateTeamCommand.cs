using MediatR;
using SportEventManager.Application.DTOs;

namespace SportEventManager.Application.Features.Teams.Commands.UpdateTeam
{
    public class UpdateTeamCommand : IRequest<TeamDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
