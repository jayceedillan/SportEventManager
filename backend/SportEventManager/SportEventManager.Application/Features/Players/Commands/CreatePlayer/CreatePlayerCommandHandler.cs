using AutoMapper;
using MediatR;
using SportEventManager.Application.DTOs;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, PlayerDto>
    {
        private readonly IGenericRepository<Player> _repository;
        private readonly IMapper _mapper;
        public CreatePlayerCommandHandler(IGenericRepository<Player> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PlayerDto> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var players = _mapper.Map<Player>(request);
            var result = await _repository.AddAsync(players).ConfigureAwait(false);

            return _mapper.Map<PlayerDto>(result);
        }
    }
}
