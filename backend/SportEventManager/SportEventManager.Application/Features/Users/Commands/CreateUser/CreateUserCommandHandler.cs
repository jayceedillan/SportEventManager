using AutoMapper;
using MediatR;
using SportEventManager.Application.DTOs;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;
using System.Transactions;

namespace SportEventManager.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                var user = new User { UserName = request.UserName, Email = request.Email };
                var createdUser = await _repository.CreateUserAsync(user, request.Password);

                //// Map UserDto to ApplicationUser
                //var user = _mapper.Map<User>(request);
                //user.UserName = request.Email;

                //// Create the user
                //var createdUser = await _repository.CreateUserAsync(user);

                // Add roles to the user
                if (createdUser != null && request.SelectedRoles != null && request.SelectedRoles.Any())
                {
                    await _repository.AddToRolesAsync(createdUser, request.SelectedRoles);
                }


                // Commit the transaction
                transaction.Complete();

                return _mapper.Map<UserDto>(createdUser);
            }

        }
    }
}
