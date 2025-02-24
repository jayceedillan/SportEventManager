using MediatR;

namespace SportEventManager.Application.Features.Sports.Commands.DeleteSport
{
    public class DeleteSportCommand : IRequest
    {
        public int Id { get; set; }
        public DeleteSportCommand(int id)
        {
            Id = id;
        }
    }
}
