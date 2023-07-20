namespace ScreenDrafts.Api.Domain.MovieAggregate.ValueObjects;
public sealed class MovieCastMemberId : ValueObject
{
    private MovieCastMemberId(DefaultIdType value)
    {
        Value = value;
    }

    private MovieCastMemberId()
    {
    }

    public DefaultIdType Value { get; private set; }

    public static MovieCastMemberId Create(DefaultIdType value)
    {
        return new MovieCastMemberId(value);
    }

    public static MovieCastMemberId CreateUnique()
    {
        return new MovieCastMemberId(NewId.NextGuid());
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
