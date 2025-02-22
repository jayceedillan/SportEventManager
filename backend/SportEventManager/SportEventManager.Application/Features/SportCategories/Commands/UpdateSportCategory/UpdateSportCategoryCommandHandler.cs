using MediatR;
using SportEventManager.Application.Exceptions;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportEventManager.Application.Features.SportCategories.Commands.UpdateSportCategory
{
    public class UpdateSportCategoryCommandHandler : IRequestHandler<UpdateSportCategoryCommand, SportCategory>
    {
        private readonly IGenericRepository<SportCategory> _repository;

        public UpdateSportCategoryCommandHandler(IGenericRepository<SportCategory> repository)
        {
            _repository = repository;
        }

        public async Task<SportCategory> Handle(UpdateSportCategoryCommand request, CancellationToken cancellationToken)
        {
            var sportCategory = await _repository.GetByIdAsync(request.Id);

            if (sportCategory == null)
                throw new NotFoundException(nameof(SportCategory), request.Id);

            sportCategory.Name = request.Name;
            sportCategory.Description = request.Description;
            sportCategory.IconUrl = request.IconUrl;
            sportCategory.ParentId = request.ParentId;

            await _repository.UpdateAsync(sportCategory);
            return sportCategory;
        }
    }
}
