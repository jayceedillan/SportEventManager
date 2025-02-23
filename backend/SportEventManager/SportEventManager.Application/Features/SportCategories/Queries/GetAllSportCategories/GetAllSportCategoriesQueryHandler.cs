using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportEventManager.Application.Common.Extensions;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.Features.SportCategories.DTOs;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;
using System.Linq.Expressions;
using System.Reflection;

namespace SportEventManager.Application.Features.SportCategories.Queries.GetAllSportCategories
{
    public class GetAllSportCategoriesQueryHandler : IRequestHandler<GetAllSportCategoriesQuery, PaginatedResult<SportCategoryDto>>
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

        public async Task<PaginatedResult<SportCategoryDto>> Handle(GetAllSportCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Fetching sports data with filter: {@Request}", request);

                var query = _repository.GetAllQueryable()
                    .ApplyDynamicFiltering(
                        request.SearchTerm,
                        new[] { "Name", "Description" }
                    )
                    .ApplyDynamicSorting(
                        request.SortBy ?? "Name",
                        request.SortDescending
                    );

                var result = await query.ToPaginatedListAsync(
                    request.PageNumber,
                    request.PageSize,
                    cancellationToken
                );

                var mappedData = _mapper.Map<List<SportCategoryDto>>(result.Items);

                return new PaginatedResult<SportCategoryDto>(
                    mappedData,
                    result.PageNumber,
                    result.PageSize,
                    result.TotalCount
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching sports data");
                throw;
            }
        }
    }
}
