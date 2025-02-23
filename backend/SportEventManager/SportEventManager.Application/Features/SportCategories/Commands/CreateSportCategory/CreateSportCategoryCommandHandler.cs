using AutoMapper;
using MediatR;
using SportEventManager.Application.DTOs;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.SportCategories.Commands.CreateSportCategory
{
    public class CreateSportCommandHandler : IRequestHandler<CreateSportCategoryCommand, SportCategoryDto>
    {
        private readonly IGenericRepository<SportCategory> _repository;
        private readonly IMapper _mapper;

        public CreateSportCommandHandler(
            IGenericRepository<SportCategory> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SportCategoryDto> Handle(CreateSportCategoryCommand request, CancellationToken cancellationToken)
        {
            var sportCategory = _mapper.Map<SportCategory>(request);
            var result = await _repository.AddAsync(sportCategory).ConfigureAwait(false);

            return _mapper.Map<SportCategoryDto>(result);
        }
    }
}