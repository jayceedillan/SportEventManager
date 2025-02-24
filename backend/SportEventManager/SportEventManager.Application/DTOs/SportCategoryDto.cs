using SportEventManager.Domain.Common;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Application.DTOs
{
    public class SportCategoryDto : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }

    }
}
