using SportEventManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportEventManager.Domain.Entities
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public ICollection<Player> Players { get; set; }
    }

}
