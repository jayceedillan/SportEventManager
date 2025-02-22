using MediatR;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.SportCategories.Queries.GetAllSportCategories
{
    public class GetAllSportCategoriesQueryHandler : IRequestHandler<GetAllSportCategoriesQuery, IReadOnlyList<SportCategory>>
    {
        private readonly IGenericRepository<SportCategory> _repository;

        public GetAllSportCategoriesQueryHandler(IGenericRepository<SportCategory> repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<SportCategory>> Handle(GetAllSportCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
