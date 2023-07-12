namespace ScreenDrafts.Api.Domain.MovieAggregate.DomainEvents;

public sealed record MovieCreatedDomainEvent(DefaultIdType Id, string MovieId) : DomainEvent(Id);
