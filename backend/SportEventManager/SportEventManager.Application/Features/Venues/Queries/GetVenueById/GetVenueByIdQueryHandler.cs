using MediatR;
using SportEventManager.Application.Exceptions;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Venues.Queries.GetVenueByIdQuery
{
    public class GetVenueByIdQueryHandler : IRequestHandler<GetVenueByIdQuery, Venue>
    {
        private readonly IGenericRepository<Venue> _repository;

        public GetVenueByIdQueryHandler(IGenericRepository<Venue> repository)
        {
            _repository = repository;
        }

        public async Task<Venue> Handle(GetVenueByIdQuery request, CancellationToken cancellationToken)
        {
            var venue = await _repository.GetByIdAsync(request.Id);

            if (venue == null)
                throw new NotFoundException(nameof(Event), request.Id);

            return venue;
        }
    }
}
