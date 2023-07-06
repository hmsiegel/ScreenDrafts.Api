namespace ScreenDrafts.Api.Domain.Identity.DomainEvents;
public sealed record ApplicationRoleDeletedEvent(
    DefaultIdType Id,
    string RoleId,
    string RoleName,
    bool PermissionsUpdated) : ApplicationRoleEvent(Id, RoleId, RoleName);
