using SportEventManager.Domain.Common;

namespace SportEventManager.Domain.Entities
{
    public class SportCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public int? ParentId { get; set; }

        // Navigation properties
        public SportCategory Parent { get; set; }
        public ICollection<SportCategory> Children { get; set; }
        public ICollection<Sport> Sports { get; set; }
    }
}
