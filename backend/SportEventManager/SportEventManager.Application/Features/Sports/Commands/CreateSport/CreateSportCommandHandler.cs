using AutoMapper;
using MediatR;
using SportEventManager.Application.DTOs;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Sports.Commands.CreateSport
{
    public class CreateSportCommandHandler : IRequestHandler<CreateSportCommand, SportDto>
    {
        private readonly IGenericRepository<Sport> _repository;
        private readonly IMapper _mapper;
        public CreateSportCommandHandler(IGenericRepository<Sport> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SportDto> Handle(CreateSportCommand request, CancellationToken cancellationToken)
        {
            var sport = _mapper.Map<Sport>(request);
            var result = await _repository.AddAsync(sport).ConfigureAwait(false);

            return _mapper.Map<SportDto>(result);

        }
    }
}
