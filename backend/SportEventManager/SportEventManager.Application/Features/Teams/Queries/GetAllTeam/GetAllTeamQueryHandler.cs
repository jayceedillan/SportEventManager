using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SportEventManager.Application.Common.Extensions;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.DTOs;
using SportEventManager.Application.Features.Teams.Queries.GetAllTeam;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Teams.Queries.GetAllTeam
{
    public class GetAllTeamsQueryHandler : IRequestHandler<GetAllTeamQuery, PaginatedResult<TeamDto>>
    {
        private readonly IGenericRepository<Team> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllTeamsQueryHandler> _logger;

        public GetAllTeamsQueryHandler(
            IGenericRepository<Team> repository,
            IMapper mapper,
            ILogger<GetAllTeamsQueryHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PaginatedResult<TeamDto>> Handle(
        GetAllTeamQuery request,
        CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Fetching Events data with filter: {@Request}", request);

                var query = _repository.GetAllQueryable()
                    .ApplyDynamicFiltering(
                        request.SearchTerm,
                        new[] { "Name" }
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

                var mappedData = _mapper.Map<List<TeamDto>>(result.Items);

                return new PaginatedResult<TeamDto>(
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
