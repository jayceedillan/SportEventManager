using SportEventManager.Domain.Common;

namespace SportEventManager.Domain.Entities
{
    public class Sport : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rules { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int? CategoryId { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public SportCategory Category { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Player> Players { get; set; }
    }

}
