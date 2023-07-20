namespace ScreenDrafts.Api.Domain.MovieAggregate.DomainEvents;

public sealed record MovieCreatedDomainEvent(DefaultIdType Id, DefaultIdType MovieId) : DomainEvent(Id);
