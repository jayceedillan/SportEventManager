using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEventManager.Domain.Entities;

namespace SportEventManager.Persistence.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasIndex(e => e.StartDate);
            builder.HasIndex(e => e.Status);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Description)
                .HasMaxLength(2000);

            builder.HasOne(e => e.Venue)
                .WithMany(v => v.Events)
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => e.Title).HasDatabaseName("idx_events_title");
            builder.HasIndex(e => e.IsDeleted);

            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }

}
