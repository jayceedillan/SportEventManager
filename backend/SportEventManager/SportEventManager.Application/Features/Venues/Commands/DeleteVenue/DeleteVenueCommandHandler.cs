using MediatR;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Venues.Commands.DeleteEvent
{

    public class DeleteVenueCommandHandler : IRequestHandler<DeleteVenueCommand>
    {
        private readonly IGenericRepository<Venue> _repository;

        public DeleteVenueCommandHandler(IGenericRepository<Venue> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteVenueCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(new Venue { Id = request.Id });
        }
    }
}
