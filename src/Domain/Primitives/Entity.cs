namespace ScreenDrafts.Api.Domain.Primitives;
public abstract class Entity : IEqualityComparer<Entity>
{
    protected Entity(DefaultIdType id)
    {
        Id = id;
    }

    protected Entity()
    {
    }

    public DefaultIdType Id { get; protected init; }

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

        if (obj is not Entity entity)
        {
            return false;
        }

        return entity.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 41;
    }

    public bool Equals(Entity? x, Entity? y)
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

    public int GetHashCode([DisallowNull] Entity obj)
    {
        return obj.Id.GetHashCode() * 41;
    }
}
