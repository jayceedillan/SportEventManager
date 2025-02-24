using MediatR;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Sports.Commands.DeleteSport
{

    public class DeleteSportCommandHandler : IRequestHandler<DeleteSportCommand>
    {
        private readonly IGenericRepository<Sport> _repository;

        public DeleteSportCommandHandler(IGenericRepository<Sport> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteSportCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(new Sport { Id = request.Id });
        }
    }
}
