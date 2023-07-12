namespace ScreenDrafts.Api.Domain.DraftAggregate.DomainEvents;

public sealed record DraftCreatedDomainEvent(DefaultIdType Id, string DraftId) : DomainEvent(Id);
