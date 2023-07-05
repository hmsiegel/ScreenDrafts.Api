namespace ScreenDrafts.Api.Domain.Identity.DomainEvents;
public abstract record ApplicationUserEvent(DefaultIdType Id, DefaultIdType UserId) : DomainEvent(Id);
