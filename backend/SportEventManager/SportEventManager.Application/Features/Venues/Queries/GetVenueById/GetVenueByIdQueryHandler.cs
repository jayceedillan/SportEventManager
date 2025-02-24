using AutoMapper;
using MediatR;
using SportEventManager.Application.DTOs;
using SportEventManager.Application.Exceptions;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Venues.Queries.GetVenueByIdQuery
{
    public class GetVenueByIdQueryHandler : IRequestHandler<GetVenueByIdQuery, VenueDto>
    {
        private readonly IGenericRepository<Venue> _repository;
        private readonly IMapper _mapper;

        public GetVenueByIdQueryHandler(IGenericRepository<Venue> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<VenueDto> Handle(GetVenueByIdQuery request, CancellationToken cancellationToken)
        {
            var venue = await _repository.GetByIdAsync(request.Id)
              ?? throw new NotFoundException(nameof(Venue), request.Id);

            return _mapper.Map<VenueDto>(venue);
        }
    }
}
