namespace ScreenDrafts.Api.Contracts.Authentication.Roles;

public sealed class RoleResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Permissions { get; set; }
}
