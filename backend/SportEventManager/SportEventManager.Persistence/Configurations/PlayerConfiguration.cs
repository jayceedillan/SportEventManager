using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Persistence.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            // Indexes
            builder.HasIndex(p => p.LRNNo);
            builder.HasIndex(p => p.SportId);
            builder.HasIndex(p => p.EventId);
            builder.HasIndex(p => p.TeamId);
            builder.HasIndex(p => p.EducationalLevelID);
            builder.HasIndex(p => p.IsDeleted);
            builder.HasQueryFilter(p => !p.IsDeleted);
            builder.HasIndex(p => p.CreatedAt)
                   .HasDatabaseName("IX_Players_CreatedAt")
                   .IsDescending();
        }
    }
}
