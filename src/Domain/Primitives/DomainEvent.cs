namespace ScreenDrafts.Api.Domain.Primitives;
public abstract record DomainEvent(DefaultIdType Id) : IDomainEvent;
