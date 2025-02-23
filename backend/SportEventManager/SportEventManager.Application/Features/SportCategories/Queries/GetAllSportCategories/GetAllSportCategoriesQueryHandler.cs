using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.Features.SportCategories.DTOs;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.SportCategories.Queries.GetAllSportCategories
{
    public class GetAllSportCategoriesQueryHandler
       : IRequestHandler<GetAllSportCategoriesQuery, PaginatedResult<SportCategoryDto>>
    {
        private readonly IGenericRepository<SportCategory> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllSportCategoriesQueryHandler> _logger;

        public GetAllSportCategoriesQueryHandler(
            IGenericRepository<SportCategory> repository,
            IMapper mapper,
            ILogger<GetAllSportCategoriesQueryHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PaginatedResult<SportCategoryDto>> Handle(
            GetAllSportCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting all sport categories with filter: {@Request}", request);

                var sportCategories = await _repository.GetFilteredAsync(
                        x => string.IsNullOrWhiteSpace(request.SearchTerm) ||
                             x.Name.ToLower().Contains(request.SearchTerm.ToLower()) ||
                             (x.Description != null && x.Description.ToLower().Contains(request.SearchTerm.ToLower())),
                        query => request.SortDescending
                            ? query.OrderByDescending(x => x.Name)
                            : query.OrderBy(x => x.Name),
                        request.PageNumber,
                        request.PageSize
                    );

                var mappedCategories = _mapper.Map<List<SportCategoryDto>>(sportCategories);


                return new PaginatedResult<SportCategoryDto>(
                    mappedCategories,
                    mappedCategories.Count,
                    request.PageNumber,
                    request.PageSize
                );

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sport categories");
                throw;
            }
        }


    }

}
