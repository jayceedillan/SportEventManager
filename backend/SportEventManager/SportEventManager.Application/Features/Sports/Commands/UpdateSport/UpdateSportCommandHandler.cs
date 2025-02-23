using AutoMapper;
using MediatR;
using SportEventManager.Application.Exceptions;
using SportEventManager.Application.Features.SportCategories.DTOs;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Sports.Commands.UpdateSport
{

    public class UpdateSportCommandHandler : IRequestHandler<UpdateSportCommand, SportDto>
    {
        private readonly IGenericRepository<Sport> _repository;
        private readonly IMapper _mapper;

        public UpdateSportCommandHandler(IGenericRepository<Sport> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SportDto> Handle(UpdateSportCommand request, CancellationToken cancellationToken)
        {
            var sportCategory = await _repository.GetByIdAsync(request.Id).ConfigureAwait(false)
                               ?? throw new NotFoundException(nameof(Sport), request.Id);

            _mapper.Map(request, sportCategory);

            await _repository.UpdateAsync(sportCategory).ConfigureAwait(false);

            return _mapper.Map<SportDto>(sportCategory);
        }
    }
}
