namespace ScreenDrafts.Api.Contracts.Authentication.Roles;
public sealed record UpdateRolePermissionsRequest(
    string RoleId,
    List<string> Permissions);
