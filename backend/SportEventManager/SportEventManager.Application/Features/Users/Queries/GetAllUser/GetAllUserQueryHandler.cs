using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SportEventManager.Application.Common.Extensions;
using SportEventManager.Application.Common.Models;
using SportEventManager.Application.DTOs;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Users.Queries.GetAllUser
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, PaginatedResult<UserDto>>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllUserQueryHandler> _logger;

        public GetAllUserQueryHandler(
            IUserRepository repository,
            IMapper mapper,
            ILogger<GetAllUserQueryHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PaginatedResult<UserDto>> Handle(
            GetAllUserQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Fetching Users data with filter: {SearchTerm}", request.SearchTerm);

                var query = _repository.GetAllUsersQueryable()
                    .ApplyDynamicFiltering(request.SearchTerm, new[] { "UserName", "Email" })
                    .ApplyDynamicSorting(request.SortBy ?? "Email", request.SortDescending);

                var result = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
                var userIds = result.Items.Select(u => u.Id).ToList();

                var userRolesMap = await _repository.GetRolesForUsersAsync(userIds);

                var mappedData = _mapper.Map<List<UserDto>>(result.Items);

                foreach (var user in mappedData)
                {
                    user.Roles = userRolesMap.GetValueOrDefault(user.Id, new List<string>());
                }

                return new PaginatedResult<UserDto>(mappedData, result.PageNumber, result.PageSize, result.TotalCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user data");
                throw;
            }
        }
    }
}
