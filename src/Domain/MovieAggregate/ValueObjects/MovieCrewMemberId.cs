namespace ScreenDrafts.Api.Domain.MovieAggregate.ValueObjects;
public sealed class MovieCrewMemberId : ValueObject
{
    private MovieCrewMemberId(DefaultIdType value)
    {
        Value = value;
    }

    private MovieCrewMemberId()
    {
    }

    public DefaultIdType Value { get; private set; }

    public static MovieCrewMemberId Create(DefaultIdType value)
    {
        return new MovieCrewMemberId(value);
    }

    public static MovieCrewMemberId CreateUnique()
    {
        return new MovieCrewMemberId(NewId.NextGuid());
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
