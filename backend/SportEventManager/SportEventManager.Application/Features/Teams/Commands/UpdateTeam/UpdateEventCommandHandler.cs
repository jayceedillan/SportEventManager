using AutoMapper;
using MediatR;
using SportEventManager.Application.DTOs;
using SportEventManager.Application.Exceptions;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Teams.Commands.UpdateTeam
{

    public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, TeamDto>
    {
        private readonly IGenericRepository<Team> _repository;
        private readonly IMapper _mapper;

        public UpdateTeamCommandHandler(IGenericRepository<Team> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TeamDto> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            var team = await _repository.GetByIdAsync(request.Id).ConfigureAwait(false)
                               ?? throw new NotFoundException(nameof(Team), request.Id);

            _mapper.Map(request, team);

            await _repository.UpdateAsync(team).ConfigureAwait(false);

            return _mapper.Map<TeamDto>(team);
        }
    }
}
