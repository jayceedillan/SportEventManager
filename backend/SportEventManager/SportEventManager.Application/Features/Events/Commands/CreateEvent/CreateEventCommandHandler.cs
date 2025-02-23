using AutoMapper;
using MediatR;
using SportEventManager.Application.DTOs;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventDto>
    {
        private readonly IGenericRepository<Event> _repository;
        private readonly IMapper _mapper;
        public CreateEventCommandHandler(IGenericRepository<Event> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EventDto> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var events = _mapper.Map<Event>(request);
            var result = await _repository.AddAsync(events).ConfigureAwait(false);

            return _mapper.Map<EventDto>(result);

        }
    }
}
