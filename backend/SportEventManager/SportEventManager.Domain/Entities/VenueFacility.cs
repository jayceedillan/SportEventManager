using SportEventManager.Domain.Common;

namespace SportEventManager.Domain.Entities
{
    public class VenueFacility:  BaseEntity
    {
        public int VenueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }

        // Navigation property
        public Venue Venue { get; set; }
    }
}
