using SportEventManager.Domain.Common;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Application.DTOs
{
    public class UserPreferenceDto : BaseEntity
    {
        public string PreferenceKey { get; set; }
        public string PreferenceValue { get; set; }

        // Navigation property
        public UserDto User { get; set; }
    }
}
