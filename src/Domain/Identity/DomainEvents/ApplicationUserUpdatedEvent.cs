namespace ScreenDrafts.Api.Domain.Identity.DomainEvents;
public sealed record ApplicationUserUpdatedEvent(
    DefaultIdType Id,
    DefaultIdType UserId,
    bool RolesUpdate = false) : ApplicationUserEvent(Id, UserId);
