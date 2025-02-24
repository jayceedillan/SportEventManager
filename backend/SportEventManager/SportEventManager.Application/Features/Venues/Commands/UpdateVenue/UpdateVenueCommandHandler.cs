using AutoMapper;
using MediatR;
using SportEventManager.Application.DTOs;
using SportEventManager.Application.Exceptions;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Venues.Commands.UpdateEvent
{

    public class UpdateVenueCommandHandler : IRequestHandler<UpdateVenueCommand, VenueDto>
    {
        private readonly IGenericRepository<Venue> _repository;
        private readonly IMapper _mapper;

        public UpdateVenueCommandHandler(IGenericRepository<Venue> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<VenueDto> Handle(UpdateVenueCommand request, CancellationToken cancellationToken)
        {
            var venue = await _repository.GetByIdAsync(request.Id).ConfigureAwait(false)
                               ?? throw new NotFoundException(nameof(Event), request.Id);

            _mapper.Map(request, venue);

            await _repository.UpdateAsync(venue).ConfigureAwait(false);

            return _mapper.Map<VenueDto>(venue);
        }
    }
}
