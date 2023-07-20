namespace ScreenDrafts.Api.Domain.DraftAggregate.DomainEvents;

public sealed record DraftCreatedDomainEvent(DefaultIdType Id, DefaultIdType DraftId) : DomainEvent(Id);
