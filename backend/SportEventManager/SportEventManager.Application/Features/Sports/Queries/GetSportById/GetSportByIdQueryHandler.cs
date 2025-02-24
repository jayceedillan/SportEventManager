using AutoMapper;
using MediatR;
using SportEventManager.Application.DTOs;
using SportEventManager.Application.Exceptions;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Sports.Queries.GetSportById
{
    public class GetEventByIdQueryHandler : IRequestHandler<GetSportByIdQuery, SportDto>
    {
        private readonly IGenericRepository<Sport> _repository;
        private readonly IMapper _mapper;

        public GetEventByIdQueryHandler(IGenericRepository<Sport> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SportDto> Handle(GetSportByIdQuery request, CancellationToken cancellationToken)
        {
     
            var sport = await _repository.GetByIdAsync(request.Id)
                       ?? throw new NotFoundException(nameof(User), request.Id);

            return _mapper.Map<SportDto>(sport);
        }
    }
}
