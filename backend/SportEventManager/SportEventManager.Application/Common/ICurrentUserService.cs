namespace SportEventManager.Application.Common
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string UserName { get; }
        string Email { get; }
        IList<string> Roles { get; }
        bool IsAuthenticated { get; }
    }
}
