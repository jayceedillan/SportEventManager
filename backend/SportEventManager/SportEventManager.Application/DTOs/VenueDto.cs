using SportEventManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportEventManager.Application.DTOs
{
    public class VenueDto : BaseEntity

    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Capacity { get; set; }
    }
}
