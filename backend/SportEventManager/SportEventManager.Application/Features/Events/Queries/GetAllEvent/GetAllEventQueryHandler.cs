using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SportEventManager.Application.Common.Extensions;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.DTOs;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Events.Queries.GetAllEvent
{
    public class GetAllEventsQueryHandler : IRequestHandler<GetAllEventQuery, PaginatedResult<EventDto>>
    {
        private readonly IGenericRepository<Event> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllEventsQueryHandler> _logger;

        public GetAllEventsQueryHandler(
            IGenericRepository<Event> repository,
            IMapper mapper,
            ILogger<GetAllEventsQueryHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PaginatedResult<EventDto>> Handle(
        GetAllEventQuery request,
        CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Fetching Events data with filter: {@Request}", request);

                var query = _repository.GetAllQueryable()
                    .ApplyDynamicFiltering(
                        request.SearchTerm,
                        new[] { "Title", "Description" }
                    )
                    .ApplyDynamicSorting(
                        request.SortBy ?? "Title",
                        request.SortDescending
                    );

                var result = await query.ToPaginatedListAsync(
                    request.PageNumber,
                    request.PageSize,
                    cancellationToken
                );

                var mappedData = _mapper.Map<List<EventDto>>(result.Items);

                return new PaginatedResult<EventDto>(
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
