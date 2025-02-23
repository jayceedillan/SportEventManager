using MediatR;
using SportEventManager.Application.Exceptions;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Players.Queries.GetPlayerByIdQuery
{
    public class GetPlayerByIdQueryHandler : IRequestHandler<GetPlayerByIdQuery, Player>
    {
        private readonly IPlayerRepository _repository;

        public GetPlayerByIdQueryHandler(IPlayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Player> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
        {
            var player = await _repository.GetPlayerById(request.Id);

            if (player == null)
                throw new NotFoundException(nameof(Player), request.Id);

            return player;
        }
    }
}
