namespace ScreenDrafts.Api.Infrastructure.Auth.Permissions;
public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string action, string resource) =>
        Policy = ScreenDraftsPermission.NameFor(action, resource);
}
