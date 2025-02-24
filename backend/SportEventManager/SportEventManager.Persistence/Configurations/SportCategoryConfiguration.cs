using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Persistence.Configurations
{
    public class SportCategoryConfiguration : IEntityTypeConfiguration<SportCategory>
    {
        public void Configure(EntityTypeBuilder<SportCategory> builder)
        {

            builder.HasIndex(sc => sc.Name).IsUnique();
            builder.HasIndex(e => e.IsDeleted);
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }

}
