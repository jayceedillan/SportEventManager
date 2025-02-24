using MediatR;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Teams.Commands.DeleteTeam
{

    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand>
    {
        private readonly IGenericRepository<Team> _repository;

        public DeleteTeamCommandHandler(IGenericRepository<Team> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(new Team { Id = request.Id });
        }
    }
}
