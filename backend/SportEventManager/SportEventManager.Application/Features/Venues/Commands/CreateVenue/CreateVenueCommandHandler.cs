using AutoMapper;
using MediatR;
using SportEventManager.Application.DTOs;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Venues.Commands.CreateEvent
{
    public class CreateVenueCommandHandler : IRequestHandler<CreateVenueCommand, VenueDto>
    {
        private readonly IGenericRepository<Venue> _repository;
        private readonly IMapper _mapper;
        public CreateVenueCommandHandler(IGenericRepository<Venue> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<VenueDto> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
        {
            var venues = _mapper.Map<Venue>(request);
            var result = await _repository.AddAsync(venues).ConfigureAwait(false);

            return _mapper.Map<VenueDto>(result);

        }
    }
}
