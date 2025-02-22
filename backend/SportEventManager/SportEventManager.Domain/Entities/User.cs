
using SportEventManager.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace SportEventManager.Domain.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Event> OrganizedEvents { get; set; }
        public ICollection<UserPreference> Preferences { get; set; }
    }
}
