namespace ScreenDrafts.Api.Application.Common.Interfaces.Services;
public interface ICurrentUserService
{
    string? Name { get; }

    DefaultIdType GetUserId();

    string? GetUserEmail();

    bool IsAuthenticated();

    bool IsInRole(string role);

    IEnumerable<Claim>? GetUserClaims();
}
