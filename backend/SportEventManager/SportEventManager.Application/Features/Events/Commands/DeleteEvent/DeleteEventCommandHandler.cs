using MediatR;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Events.Commands.DeleteEvent
{

    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly IGenericRepository<Event> _repository;

        public DeleteEventCommandHandler(IGenericRepository<Event> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(new Event { Id = request.Id });
        }
    }
}
