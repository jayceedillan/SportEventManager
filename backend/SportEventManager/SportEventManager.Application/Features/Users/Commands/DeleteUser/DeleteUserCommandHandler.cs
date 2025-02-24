using MediatR;
using Microsoft.AspNetCore.Identity;
using SportEventManager.Application.Exceptions;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Application.Features.Users.Commands.DeleteUser
{

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _repository;
        private readonly UserManager<User> _userManager;
        public DeleteUserCommandHandler(IUserRepository repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            // Step 1: Fetch the user asynchronously with error handling
            var user = await _repository.GetUserByIdAsync(request.Id).ConfigureAwait(false)
                              ?? throw new NotFoundException(nameof(User), request.Id);

            // Step 2: Ensure that the last admin user is not deleted
            await ValidateDeleteUserIsNotLastAdminAsync(user);


            // Step 3: Proceed with deletion and handle errors
            await _userManager.DeleteAsync(user);


        }

        // Step 2: Validate that the user is not the last admin
        private async Task ValidateDeleteUserIsNotLastAdminAsync(User user)
        {

            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Contains("Admin"))
            {
                var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");

                if (adminUsers.Count == 1)
                {
                    throw new NotFoundException(nameof(User), user.Id);
                }
            }

        }

    }
}
