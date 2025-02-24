using MediatR;

namespace SportEventManager.Application.Features.Players.Commands.DeletePlayer
{
    public class DeletePlayerCommand : IRequest
    {
        public int Id { get; set; }
        public DeletePlayerCommand(int id)
        {
            Id = id;
        }
    }
}
