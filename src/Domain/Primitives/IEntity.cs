namespace ScreenDrafts.Api.Domain.Primitives;
public interface IEntity
{
    List<DomainEvent> DomainEvents { get; }
}

public interface IEntity<out TId> : IEntity
{
    TId Id { get; }
}
