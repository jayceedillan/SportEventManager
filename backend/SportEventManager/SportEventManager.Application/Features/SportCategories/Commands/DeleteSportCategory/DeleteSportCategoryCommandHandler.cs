using MediatR;
using SportEventManager.Application.Exceptions;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.SportCategories.Commands.DeleteSportCategory
{
    public class DeleteSportCategoryCommandHandler : IRequest<DeleteSportCategoryCommand>
    {
        private readonly IGenericRepository<SportCategory> _repository;

        public DeleteSportCategoryCommandHandler(IGenericRepository<SportCategory> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteSportCategoryCommand request, CancellationToken cancellationToken)
        {
            var sportCategory = await _repository.GetByIdAsync(request.Id);

            if (sportCategory == null)
                throw new NotFoundException(nameof(SportCategory), request.Id);

            await _repository.DeleteAsync(sportCategory);
            return Unit.Value;
        }
    }
}
