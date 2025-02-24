using SportEventManager.Domain.Common;

namespace SportEventManager.Domain.Entities
{
    public class SportCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }

        // Navigation properties
        public ICollection<Sport> Sports { get; set; }
    }
}
