using SportEventManager.Domain.Common;

namespace SportEventManager.Domain.Entities
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }
       
        // Navigation properties
        public ICollection<Player> Players { get; set; }
    }

}
