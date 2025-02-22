using MediatR;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.SportCategories.Commands.CreateSportCategory
{
    public class CreateSportCommandHandler : IRequestHandler<CreateSportCategoryCommand, SportCategory>
    {
        private readonly IGenericRepository<SportCategory> _repository;

        public CreateSportCommandHandler(IGenericRepository<SportCategory> repository)
        {
            _repository = repository;
        }

        public async Task<SportCategory> Handle(CreateSportCategoryCommand request, CancellationToken cancellationToken)
        {
            var sportCategory = new SportCategory
            {
                Name = request.Name,
                Description = request.Description,
                IconUrl = request.IconUrl,
                ParentId = request.ParentId
            };

            return await _repository.AddAsync(sportCategory);
        }
    }
}
