namespace ScreenDrafts.Api.Domain.Identity.DomainEvents;

public record ApplicationUserCreatedEvent(DefaultIdType Id, DefaultIdType UserId) : ApplicationUserEvent(Id, UserId);
