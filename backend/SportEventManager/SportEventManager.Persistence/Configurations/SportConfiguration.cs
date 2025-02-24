using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Persistence.Configurations
{
    public class SportConfiguration : IEntityTypeConfiguration<Sport>
    {
        public void Configure(EntityTypeBuilder<Sport> builder)
        {
            builder.HasIndex(s => s.Name).HasDatabaseName("idx_sports_name");
            builder.HasIndex(s => s.IsDeleted);
            builder.HasIndex(s => s.Name).IsUnique();
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
