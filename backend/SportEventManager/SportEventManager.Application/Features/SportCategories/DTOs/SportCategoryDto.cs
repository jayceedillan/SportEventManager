using SportEventManager.Domain.Common;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Application.Features.SportCategories.DTOs
{
    public class SportCategoryDto : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public int? ParentId { get; set; }

        public SportCategory Parent { get; set; }
        public ICollection<SportCategory> Children { get; set; }
        public ICollection<Sport> Sports { get; set; }
    }
}
