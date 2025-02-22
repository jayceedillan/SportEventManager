using SportEventManager.Domain.Common;

namespace SportEventManager.Domain.Entities
{
    public class Player : BaseEntity
    {
        public int PlayerId { get; set; }
        public int SportId { get; set; }
        public int? EventId { get; set; }
        public int? TeamId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Status { get; set; } = "Pending";
        public string PaymentStatus { get; set; } = "Unpaid";

        // Navigation properties
        //public User User { get; set; }
        public Sport Sport { get; set; }
        public Event Event { get; set; }
        public Team Team { get; set; }
    }
}
