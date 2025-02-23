using AutoMapper;
using MediatR;
using SportEventManager.Application.DTOs;
using SportEventManager.Application.Exceptions;
using SportEventManager.Application.Features.Venues.Commands.UpdateEvent;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Events.Commands.UpdateEvent
{

    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, EventDto>
    {
        private readonly IGenericRepository<Event> _repository;
        private readonly IMapper _mapper;

        public UpdateEventCommandHandler(IGenericRepository<Event> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EventDto> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var events = await _repository.GetByIdAsync(request.Id).ConfigureAwait(false)
                               ?? throw new NotFoundException(nameof(Event), request.Id);

            _mapper.Map(request, events);

            await _repository.UpdateAsync(events).ConfigureAwait(false);

            return _mapper.Map<EventDto>(events);
        }
    }
}
