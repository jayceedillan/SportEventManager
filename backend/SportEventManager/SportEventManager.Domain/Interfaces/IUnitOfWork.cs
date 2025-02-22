using SportEventManager.Domain.Interfaces.IRepositories;

namespace SportEventManager.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISportCategoryRepository SportCategories { get; }
        //ISportRepository Sports { get; }
        //IVenueRepository Venues { get; }
        //IEventRepository Events { get; }
        //ITeamRepository Teams { get; }
        //IPlayerRepository Players { get; }
        //IVenueFacilityRepository VenueFacilities { get; }
        //IEventScheduleRepository EventSchedules { get; }
        //IUserPreferenceRepository UserPreferences { get; }

        Task<int> CompleteAsync();
    }
}
