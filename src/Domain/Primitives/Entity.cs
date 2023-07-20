namespace ScreenDrafts.Api.Domain.Primitives;
public abstract class Entity<TId> : IEqualityComparer<Entity<TId>>, IHasDomainEvent
    where TId : ValueObject
{
    private readonly List<IDomainEvent> _domainEvents = new();
    protected Entity(TId id)
    {
        Id = id;
    }

    protected Entity()
    {
    }

    public TId? Id { get; protected init; }
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (obj is not Entity<TId> entity)
        {
            return false;
        }

        return entity.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id!.GetHashCode() * 41;
    }
    
    public int GetHashCode([DisallowNull] Entity<TId> obj)
    {
        return obj.Id!.GetHashCode() * 41;
    }

    public bool Equals(Entity<TId>? x, Entity<TId>? y)
    {
        if (x is null && y is null)
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        return x.Equals(y);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
