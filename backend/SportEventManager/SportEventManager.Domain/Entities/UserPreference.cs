using SportEventManager.Domain.Common;

namespace SportEventManager.Domain.Entities
{
    public class UserPreference : BaseEntity
    {

        public string PreferenceKey { get; set; }
        public string PreferenceValue { get; set; }

        // Navigation property
        public User User { get; set; }

    }
}
