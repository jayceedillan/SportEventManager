using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore; // ✅ Fix for CountAsync
using Microsoft.Extensions.Logging;
using SportEventManager.Application.Common.Extensions;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.Features.SportCategories.DTOs;
using SportEventManager.Application.Features.SportCategories.Queries.GetAllSportCategories;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;
using System.Linq.Expressions;
using System.Reflection;

namespace SportEventManager.Application.Features.Sports.Queries.GetAllSport
{
    public class GetAllSportQueryHandler : IRequestHandler<GetAllSportQuery, PaginatedResult<SportDto>>
    {
        private readonly IGenericRepository<Sport> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllSportQueryHandler> _logger;

        public GetAllSportQueryHandler(
            IGenericRepository<Sport> repository,
            IMapper mapper,
            ILogger<GetAllSportQueryHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PaginatedResult<SportDto>> Handle(
        GetAllSportQuery request,
        CancellationToken cancellationToken)
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

                var mappedData = _mapper.Map<List<SportDto>>(result.Items);

                return new PaginatedResult<SportDto>(
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
