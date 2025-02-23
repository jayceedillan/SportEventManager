using SportEventManager.Domain.Common;

namespace SportEventManager.Domain.Entities
{
    public class Venue : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Capacity { get; set; }

        // Navigation properties
        public ICollection<Event> Events { get; set; }
        public ICollection<VenueFacility> Facilities { get; set; }
        public ICollection<EventSchedule> Schedules { get; set; }
    }
}
