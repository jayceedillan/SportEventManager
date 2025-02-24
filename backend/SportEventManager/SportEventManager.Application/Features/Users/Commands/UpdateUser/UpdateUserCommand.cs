using MediatR;
using SportEventManager.Application.DTOs;

namespace SportEventManager.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UserDto>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> SelectedRoles { get; set; }
    }
}
