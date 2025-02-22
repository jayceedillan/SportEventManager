using AutoMapper;
using MediatR;
using SportEventManager.Application.Exceptions;
using SportEventManager.Application.Features.SportCategories.DTOs;
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
            try
            {
                if (request.ParentId.HasValue)
                {
                    var parentExists = await _repository.GetByIdAsync(request.ParentId.Value);
                    if (parentExists == null)
                        throw new NotFoundException(nameof(SportCategory), request.ParentId);
                }

                var sportCategory = new SportCategory
                {
                    Name = request.Name,
                    Description = request.Description,
                    IconUrl = request.IconUrl,
                    ParentId = request.ParentId
                };

                var result = await _repository.AddAsync(sportCategory);
                return _mapper.Map<SportCategoryDto>(result);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error creating sport category", ex);
            }
        }
    }
}