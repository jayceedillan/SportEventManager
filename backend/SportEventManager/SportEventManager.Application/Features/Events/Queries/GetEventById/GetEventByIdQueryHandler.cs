using MediatR;
using SportEventManager.Application.Exceptions;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Events.Queries.GetEventById
{
    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, Event>
    {
        private readonly IGenericRepository<Event> _repository;

        public GetEventByIdQueryHandler(IGenericRepository<Event> repository)
        {
            _repository = repository;
        }

        public async Task<Event> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var events = await _repository.GetByIdAsync(request.Id);

            if (events == null)
                throw new NotFoundException(nameof(Event), request.Id);

            return events;
        }
    }
}
