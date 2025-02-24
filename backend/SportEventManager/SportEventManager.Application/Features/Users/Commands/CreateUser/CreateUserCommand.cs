using MediatR;
using SportEventManager.Application.DTOs;

namespace SportEventManager.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> SelectedRoles { get; set; }
    }
}
