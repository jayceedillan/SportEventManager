using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Persistence.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasIndex(t => t.Name).IsUnique();
            builder.HasIndex(e => e.IsDeleted);
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
