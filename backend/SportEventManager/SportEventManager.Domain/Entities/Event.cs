﻿using SportEventManager.Domain.Common;

namespace SportEventManager.Domain.Entities
{
    public class Event : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? VenueId { get; set; }
        public int SportId { get; set; }
        public string OrganizerId { get; set; }
        public string Status { get; set; } = "Scheduled";
        public int? MaxParticipants { get; set; }
        public decimal? RegistrationFee { get; set; }

        public Venue Venue { get; set; }
        public Sport Sport { get; set; }
        public User Organizer { get; set; }
        public ICollection<Player> Players { get; set; }
        public ICollection<EventSchedule> Schedules { get; set; }
    }
}
