

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SportEventManager.Application.Common.Extensions;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.DTOs;

using SportEventManager.Domain.Interfaces.IRepositories;
using SportEventManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SportEventManager.Application.Features.Players.Queries.GetAllPlayer
{
    public class GetAllPlayerQueryHandler : IRequestHandler<GetAllPlayerQuery, PaginatedResult<PlayerDto>>
    {
        private readonly IGenericRepository<Player> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPlayerQueryHandler> _logger;

        public GetAllPlayerQueryHandler(
            IGenericRepository<Player> repository,
            IMapper mapper,
            ILogger<GetAllPlayerQueryHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PaginatedResult<PlayerDto>> Handle(
        GetAllPlayerQuery request,
        CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Fetching Player data with filter: {@Request}", request);

                var query = _repository.GetAllQueryable()
                         .Include(p => p.Sport)
                         .Include(p => p.Event)
                         .Include(p => p.Team)
                         .ApplyDynamicFiltering(
                             request.SearchTerm,
                             new[] { "FirstName", "LastName", "EmailAddress" }
                         )
                         .ApplyDynamicSorting(
                             request.SortBy ?? "FirstName",
                             request.SortDescending
                         )
                    .ApplyDynamicSorting(
                        request.SortBy ?? "FirstName",
                        request.SortDescending
                    );

                var result = await query.ToPaginatedListAsync(
                    request.PageNumber,
                    request.PageSize,
                    cancellationToken
                );

                var mappedData = _mapper.Map<List<PlayerDto>>(result.Items);

                return new PaginatedResult<PlayerDto>(
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
