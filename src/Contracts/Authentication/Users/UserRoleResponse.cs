namespace ScreenDrafts.Api.Contracts.Authentication.Users;
public sealed record UserRoleResponse(
    string RoleId,
    string RoleName,
    string RoleDescription,
    bool IsEnabled);
