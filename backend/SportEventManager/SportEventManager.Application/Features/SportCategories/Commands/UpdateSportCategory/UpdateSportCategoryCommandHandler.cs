using AutoMapper;
using MediatR;
using SportEventManager.Application.DTOs;
using SportEventManager.Application.Exceptions;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.SportCategories.Commands.UpdateSportCategory
{
    public class UpdateSportCategoryCommandHandler : IRequestHandler<UpdateSportCategoryCommand, SportCategoryDto>
    {
        private readonly IGenericRepository<SportCategory> _repository;
        private readonly IMapper _mapper;

        public UpdateSportCategoryCommandHandler(IGenericRepository<SportCategory> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SportCategoryDto> Handle(UpdateSportCategoryCommand request, CancellationToken cancellationToken)
        {
            var sportCategory = await _repository.GetByIdAsync(request.Id).ConfigureAwait(false)
                               ?? throw new NotFoundException(nameof(SportCategory), request.Id);

            _mapper.Map(request, sportCategory);

            await _repository.UpdateAsync(sportCategory).ConfigureAwait(false);

            return _mapper.Map<SportCategoryDto>(sportCategory);
        }
    }
}
