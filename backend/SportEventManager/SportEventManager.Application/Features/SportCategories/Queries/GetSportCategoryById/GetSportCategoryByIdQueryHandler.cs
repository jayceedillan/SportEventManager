using MediatR;
using SportEventManager.Application.Exceptions;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.SportCategories.Queries.GetSportCategoryById
{
    public class GetSportCategoryByIdQueryHandler : IRequestHandler<GetSportCategoryByIdQuery, SportCategory>
    {
        private readonly IGenericRepository<SportCategory> _repository;

        public GetSportCategoryByIdQueryHandler(IGenericRepository<SportCategory> repository)
        {
            _repository = repository;
        }

        public async Task<SportCategory> Handle(GetSportCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var sportCategory = await _repository.GetByIdAsync(request.Id);

            if (sportCategory == null)
                throw new NotFoundException(nameof(SportCategory), request.Id);

            return sportCategory;
        }
    }
}
