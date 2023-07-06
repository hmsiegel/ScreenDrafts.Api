namespace ScreenDrafts.Api.Contracts.Authentication.Users;

public sealed class UserRolesRequest
{
    public List<UserRoleResponse> UserRoles { get; set; } = new ();
}
