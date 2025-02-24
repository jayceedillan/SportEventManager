using Microsoft.AspNetCore.Identity;

namespace SportEventManager.Application.DTOs
{
    public class UserDto : IdentityUser
    {
        public List<string> SelectedRoles { get; set; }
        public List<string> Roles { get; set; }
    }
}
