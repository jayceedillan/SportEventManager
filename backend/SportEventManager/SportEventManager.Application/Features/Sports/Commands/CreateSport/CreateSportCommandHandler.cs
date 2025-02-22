using MediatR;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Sports.Commands.CreateSport
{
    public class CreateSportCommandHandler : IRequestHandler<CreateSportCommand, Sport>
    {
        private readonly IGenericRepository<Sport> _repository;

        public CreateSportCommandHandler(IGenericRepository<Sport> repository)
        {
            _repository = repository;
        }

        public async Task<Sport> Handle(CreateSportCommand request, CancellationToken cancellationToken)
        {
            var sport = new Sport
            {
                Name = request.Name,
                Description = request.Description,

                Rules= request.Rules,
                MinPlayers = request.MinPlayers,
                MaxPlayers = request.MaxPlayers,
                CategoryId = request.CategoryId,
                IsActive = request.IsActive

      

    };

            return await _repository.AddAsync(sport);
        }
    }
}
