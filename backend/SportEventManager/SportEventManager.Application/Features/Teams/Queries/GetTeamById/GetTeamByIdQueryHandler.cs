using MediatR;
using SportEventManager.Application.Exceptions;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Teams.Queries.GetTeamById
{
    public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, Team>
    {
        private readonly IGenericRepository<Team> _repository;

        public GetTeamByIdQueryHandler(IGenericRepository<Team> repository)
        {
            _repository = repository;
        }

        public async Task<Team> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
        {
            var team = await _repository.GetByIdAsync(request.Id);
            
            if (team == null)
                throw new NotFoundException(nameof(Team), request.Id);

            return team;
        }
    }
}
