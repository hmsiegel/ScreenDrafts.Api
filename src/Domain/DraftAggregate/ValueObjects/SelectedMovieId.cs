namespace ScreenDrafts.Api.Domain.DraftAggregate.ValueObjects;
public sealed class SelectedMovieId : ValueObject
{
    private SelectedMovieId(DefaultIdType value)
    {
        Value = value;
    }

    private SelectedMovieId()
    {
    }

    public DefaultIdType Value { get; private set; }

    public static SelectedMovieId Create(DefaultIdType value)
    {
        return new SelectedMovieId(value);
    }

    public static SelectedMovieId CreateUnique()
    {
        return new SelectedMovieId(NewId.NextGuid());
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
