using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportEventManager.Application.Common;
using SportEventManager.Domain.Common;
using SportEventManager.Domain.Entities;
using SportEventManager.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SportEventManager.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        private readonly ICurrentUserService? _currentUserService;
        private readonly IDateTime? _dateTime;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            ICurrentUserService currentUserService,
            IDateTime dateTime)
            : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public DbSet<SportCategory> SportCategories { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<VenueFacility> VenueFacilities { get; set; }
        public DbSet<EventSchedule> EventSchedules { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasIndex(u => u.Email).HasDatabaseName("idx_users_email");
            builder.Entity<User>().HasIndex(u => u.UserName).HasDatabaseName("idx_users_username");
            builder.Entity<Sport>().HasIndex(s => s.Name).HasDatabaseName("idx_sports_name");
            builder.Entity<Event>().HasIndex(e => e.Title).HasDatabaseName("idx_events_title");
            builder.Entity<SportCategory>().HasIndex(sc => sc.Name).IsUnique();
            builder.Entity<Sport>().HasIndex(s => s.Name).IsUnique();
            builder.Entity<Team>().HasIndex(t => t.Name).IsUnique();

            builder.Entity<Event>().Property(e => e.RegistrationFee).HasPrecision(10, 2);
            builder.Entity<Venue>().Property(v => v.Latitude).HasPrecision(9, 6);
            builder.Entity<Venue>().Property(v => v.Longitude).HasPrecision(9, 6);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                string userId = _currentUserService?.UserId ?? "System";
                DateTime currentDateTime = _dateTime?.Now ?? DateTime.UtcNow;

                foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.CreatedAt = currentDateTime;
                            entry.Entity.CreatedBy = userId;
                            entry.Entity.IsDeleted = false;
                            break;
                        case EntityState.Modified:
                            entry.Entity.UpdatedAt = currentDateTime;
                            entry.Entity.UpdatedBy = userId;
                            entry.Property(x => x.CreatedAt).IsModified = false;
                            entry.Property(x => x.CreatedBy).IsModified = false;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.Entity.IsDeleted = true;
                            entry.Entity.UpdatedAt = currentDateTime;
                            entry.Entity.UpdatedBy = userId;
                            break;
                    }
                }

                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException("A concurrency conflict occurred.", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new DatabaseOperationException("An error occurred while saving changes.", ex);
            }
            catch (ValidationException ex)
            {
                throw new EntityValidationException("One or more validation errors occurred.", ex);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
                    .UseLazyLoadingProxies();
            }
        }

        public void DetachAllEntities()
        {
            foreach (var entry in ChangeTracker.Entries().Where(e => e.State != EntityState.Detached))
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
