namespace ScreenDrafts.Api.Domain.Primitives;
public abstract class ValueObject : IEqualityComparer<ValueObject>
{
    protected ValueObject()
    {
    }

    public abstract IEnumerable<object> GetAtomicValues();

    public override bool Equals(object? obj)
    {
        return obj is ValueObject other && ValuesAreEqual(other);
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Aggregate(
            default(int),
            HashCode.Combine);
    }

    public bool Equals(ValueObject? x, ValueObject? y)
    {
        if (x is null && y is null)
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        return false;
    }

    public int GetHashCode([DisallowNull] ValueObject obj)
    {
        return obj.GetHashCode();
    }

    private bool ValuesAreEqual(ValueObject other)
    {
        return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }
}
