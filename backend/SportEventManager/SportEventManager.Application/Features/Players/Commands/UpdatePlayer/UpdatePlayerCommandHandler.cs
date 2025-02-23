using AutoMapper;
using MediatR;
using SportEventManager.Application.DTOs;
using SportEventManager.Application.Exceptions;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Players.Commands.UpdatePlayer
{

    public class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand, PlayerDto>
    {
        private readonly IGenericRepository<Player> _repository;
        private readonly IMapper _mapper;

        public UpdatePlayerCommandHandler(IGenericRepository<Player> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PlayerDto> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = await _repository.GetByIdAsync(request.Id).ConfigureAwait(false)
                               ?? throw new NotFoundException(nameof(Player), request.Id);

            _mapper.Map(request, player);

            await _repository.UpdateAsync(player).ConfigureAwait(false);

            return _mapper.Map<PlayerDto>(player);
        }
    }
}
