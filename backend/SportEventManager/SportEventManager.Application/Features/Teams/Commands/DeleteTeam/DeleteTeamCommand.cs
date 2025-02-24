using MediatR;

namespace SportEventManager.Application.Features.Teams.Commands.DeleteTeam
{
    public class DeleteTeamCommand : IRequest
    {
        public int Id { get; set; }
        public DeleteTeamCommand(int id)
        {
            Id = id;
        }
    }
}
