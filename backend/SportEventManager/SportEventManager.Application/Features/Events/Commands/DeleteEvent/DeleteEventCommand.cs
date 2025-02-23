using MediatR;

namespace SportEventManager.Application.Features.Events.Commands.DeleteEvent
{
    public class DeleteEventCommand : IRequest
    {
        public int Id { get; set; }
        public DeleteEventCommand(int id)
        {
            Id = id;
        }
    }
}
