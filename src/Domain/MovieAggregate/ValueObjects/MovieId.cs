namespace ScreenDrafts.Api.Domain.MovieAggregate.ValueObjects;
public sealed class MovieId : AggregateRootId<DefaultIdType>
{
    private MovieId(DefaultIdType value)
    {
        Value = value;
    }

    private MovieId()
    {
    }

    public override DefaultIdType Value { get; protected set; }

    public static MovieId Create(DefaultIdType value)
    {
        return new MovieId(value);
    }

    public static MovieId CreateUnique()
    {
        return new MovieId(NewId.NextGuid());
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
