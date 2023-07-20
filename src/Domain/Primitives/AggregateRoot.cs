namespace ScreenDrafts.Api.Domain.Primitives;

public abstract class AggregateRoot<TId, TIdType> : Entity<TId>
    where TId : AggregateRootId<TIdType>
{
    protected AggregateRoot(TId id)
    : base(id)
    {
        Id = id;
    }

    protected AggregateRoot()
    {
    }
}
