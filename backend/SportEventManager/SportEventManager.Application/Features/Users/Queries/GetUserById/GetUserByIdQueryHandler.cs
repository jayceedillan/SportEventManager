using AutoMapper;
using MediatR;
using SportEventManager.Application.DTOs;
using SportEventManager.Application.Exceptions;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Users.Queries.GetUserByIdQuery
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByIdAsync(request.Id)
                      ?? throw new NotFoundException(nameof(User), request.Id);

            var userRolesMap = await _repository.GetRolesForUsersAsync(new List<string> { user.Id });
            var roles = userRolesMap.TryGetValue(user.Id, out var userRoles) ? userRoles : new List<string>();

            var mappedUser = _mapper.Map<UserDto>(user);
            mappedUser.Roles = roles;

            return mappedUser;
        }
    }
}
