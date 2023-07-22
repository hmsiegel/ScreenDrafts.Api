namespace ScreenDrafts.Api.Contracts.Authentication.Users;

public class UserRolesRequest
{
    public List<UserRoleResponse> UserRoles { get; set; } = new ();
}
