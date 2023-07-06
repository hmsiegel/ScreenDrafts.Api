namespace ScreenDrafts.Api.Infrastructure.Auth;
public sealed class CurrentUserService : ICurrentUserService, ICurrentUserInitializer
{
    private ClaimsPrincipal? _user;
    private Guid _userId = DefaultIdType.Empty;

    public string? Name => _user?.Identity?.Name;

    public IEnumerable<Claim>? GetUserClaims()
    {
        return _user?.Claims;
    }

    public string? GetUserEmail()
    {
        return IsAuthenticated()
            ? _user!.GetEmail()
            : string.Empty;
    }

    public DefaultIdType GetUserId()
    {
        return IsAuthenticated()
            ? DefaultIdType.Parse(_user?.GetUserId() ?? DefaultIdType.Empty.ToString())
            : _userId;
    }

    public bool IsAuthenticated()
    {
        return _user?.Identity?.IsAuthenticated is true;
    }

    public bool IsInRole(string role)
    {
        return _user?.IsInRole(role) is true;
    }

    public void SetCurrentUser(ClaimsPrincipal user)
    {
        if (_user != null)
        {
            throw new InternalServerException("Method reserved for in-scope initialization only");
        }

        _user = user;
    }

    public void SetCurrentUserId(DefaultIdType userId)
    {
        if (_userId != DefaultIdType.Empty)
        {
            throw new InternalServerException("Method reserved for in-scope initialization only");
        }

        if (!string.IsNullOrEmpty(userId.ToString()))
        {
            _userId = DefaultIdType.Parse(userId.ToString());
        }
    }
}
