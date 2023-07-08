namespace ScreenDrafts.Api.Infrastructure.Auth;
public interface ICurrentUserInitializer
{
    void SetCurrentUser(ClaimsPrincipal user);
    void SetCurrentUserId(DefaultIdType userId);
}
