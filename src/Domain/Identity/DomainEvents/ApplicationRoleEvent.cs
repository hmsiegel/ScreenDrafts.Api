namespace ScreenDrafts.Api.Domain.Identity.DomainEvents;
public abstract record ApplicationRoleEvent(DefaultIdType Id, string RoleId, string RoleName) : DomainEvent(Id);
