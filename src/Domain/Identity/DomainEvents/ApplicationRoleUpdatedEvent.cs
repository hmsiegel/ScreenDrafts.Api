namespace ScreenDrafts.Api.Domain.Identity.DomainEvents;
public sealed record ApplicationRoleUpdatedEvent(
    DefaultIdType Id,
    string RoleId,
    string RoleName,
    bool PermissionsUpdated = false) : ApplicationRoleEvent(Id, RoleId, RoleName);
