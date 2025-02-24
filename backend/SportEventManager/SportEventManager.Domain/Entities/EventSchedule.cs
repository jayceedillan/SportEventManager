using SportEventManager.Domain.Common;

namespace SportEventManager.Domain.Entities
{
    public class EventSchedule : BaseEntity
    {
        public int EventId { get; set; }
        public int VenueId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ActivityType { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }

        // Navigation properties
        public Event Event { get; set; }
        public Venue Venue { get; set; }
    }
}
