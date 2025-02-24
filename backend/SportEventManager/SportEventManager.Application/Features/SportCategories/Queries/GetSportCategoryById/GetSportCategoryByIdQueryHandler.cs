using AutoMapper;
using MediatR;
using SportEventManager.Application.DTOs;
using SportEventManager.Application.Exceptions;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.SportCategories.Queries.GetSportCategoryById
{
    public class GetSportCategoryByIdQueryHandler : IRequestHandler<GetSportCategoryByIdQuery, SportCategoryDto>
    {
        private readonly IGenericRepository<SportCategory> _repository;
        private readonly IMapper _mapper;

        public GetSportCategoryByIdQueryHandler(IGenericRepository<SportCategory> repository, IMapper mapper)
        {
            repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SportCategoryDto> Handle(GetSportCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var sportCategory = await _repository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(SportCategory), request.Id);

            return _mapper.Map<SportCategoryDto>(sportCategory);
        }
    }
}
