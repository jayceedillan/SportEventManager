using MediatR;
using SportEventManager.Application.DTOs;

namespace SportEventManager.Application.Features.Players.Commands.UpdatePlayer
{
    public class UpdatePlayerCommand : IRequest<PlayerDto>
    {
        public int Id { get; set; }
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
    }
}
