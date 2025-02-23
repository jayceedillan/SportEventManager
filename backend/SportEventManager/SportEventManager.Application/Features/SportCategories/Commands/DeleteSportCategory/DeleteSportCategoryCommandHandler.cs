using MediatR;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.SportCategories.Commands.DeleteSportCategory
{
    public class DeleteSportCategoryCommandHandler : IRequestHandler<DeleteSportCategoryCommand>
    {
        private readonly IGenericRepository<SportCategory> _repository;

        public DeleteSportCategoryCommandHandler(IGenericRepository<SportCategory> repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteSportCategoryCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(new SportCategory { Id = request.Id });
        }
    }
}
