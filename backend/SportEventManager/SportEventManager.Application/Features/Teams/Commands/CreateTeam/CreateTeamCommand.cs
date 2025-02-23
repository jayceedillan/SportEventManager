using MediatR;
using SportEventManager.Application.DTOs;

namespace SportEventManager.Application.Features.Teams.Commands.CreateTeam
{
    public class CreateTeamCommand : IRequest<TeamDto>
    {
        public string Name { get; set; }
    }
}
