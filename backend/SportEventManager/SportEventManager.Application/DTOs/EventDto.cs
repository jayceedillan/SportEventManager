using SportEventManager.Domain.Common;

namespace SportEventManager.Application.DTOs
{
    public class EventDto : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? VenueId { get; set; }
        public string Status { get; set; } = "Scheduled";
        public int? MaxParticipants { get; set; }
    }
}
