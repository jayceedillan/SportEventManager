using MediatR;

namespace SportEventManager.Application.Features.Venues.Commands.DeleteEvent
{
    public class DeleteVenueCommand : IRequest
    {
        public int Id { get; set; }
        public DeleteVenueCommand(int id)
        {
            Id = id;
        }
    }
}
