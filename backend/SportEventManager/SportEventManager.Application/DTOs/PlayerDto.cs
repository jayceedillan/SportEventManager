using SportEventManager.Domain.Common;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Application.DTOs
{
    public class PlayerDto : BaseEntity
    {
        public string LRNNo { get; set; }
        public string? ProfilePic { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string? ContactNo { get; set; }
        public string? EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string? Height { get; set; }
        public string? Weight { get; set; }

        public int SportId { get; set; }
        public int? EventId { get; set; }
        public int? TeamId { get; set; }
        public int EducationalLevelID { get; set; }

        public EducationalLevel EducationalLevel { get; set; }
        public Sport Sport { get; set; }
        public Event Event { get; set; }
        public Team Team { get; set; }
    }
}
