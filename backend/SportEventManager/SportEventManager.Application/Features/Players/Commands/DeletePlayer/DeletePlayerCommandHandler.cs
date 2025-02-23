using MediatR;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Players.Commands.DeletePlayer
{

    public class DeletePlayerCommandHandler : IRequestHandler<DeletePlayerCommand>
    {
        private readonly IGenericRepository<Player> _repository;

        public DeletePlayerCommandHandler(IGenericRepository<Player> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(new Player { Id = request.Id });
        }
    }
}
