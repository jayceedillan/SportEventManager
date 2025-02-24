using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Persistence.Configurations
{
    public class VenueConfiguration : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            builder.Property(v => v.Latitude).HasPrecision(9, 6);
            builder.Property(v => v.Longitude).HasPrecision(9, 6);
            builder.HasIndex(e => e.IsDeleted);
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
