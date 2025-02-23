

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SportEventManager.Application.Common.Extensions;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.DTOs;

using SportEventManager.Domain.Interfaces.IRepositories;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Application.Features.Venues.Queries.GetAllVenue
{
    public class GetAllVenueQueryHandler : IRequestHandler<GetAllVenueQuery, PaginatedResult<VenueDto>>
    {
        private readonly IGenericRepository<Venue> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllVenueQueryHandler> _logger;

        public GetAllVenueQueryHandler(
            IGenericRepository<Venue> repository,
            IMapper mapper,
            ILogger<GetAllVenueQueryHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PaginatedResult<VenueDto>> Handle(
        GetAllVenueQuery request,
        CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Fetching Venue data with filter: {@Request}", request);

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

                var mappedData = _mapper.Map<List<VenueDto>>(result.Items);

                return new PaginatedResult<VenueDto>(
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
