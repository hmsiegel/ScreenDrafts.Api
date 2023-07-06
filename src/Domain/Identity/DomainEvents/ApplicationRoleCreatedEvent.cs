namespace ScreenDrafts.Api.Domain.Identity.DomainEvents;
public sealed record ApplicationRoleCreatedEvent(DefaultIdType Id, string RoleId, string RoleName) : ApplicationRoleEvent(Id, RoleId, RoleName);
