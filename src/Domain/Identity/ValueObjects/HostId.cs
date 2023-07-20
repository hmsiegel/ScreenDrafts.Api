namespace ScreenDrafts.Api.Domain.Identity.ValueObjects;
public sealed class HostId : AggregateRootId<DefaultIdType>
{
    private HostId(DefaultIdType value)
    {
        Value = value;
    }

    private HostId()
    {
    }

    public override DefaultIdType Value { get; protected set; }

    public static HostId Create(DefaultIdType value)
    {
        return new HostId(value);
    }

    public static HostId CreateUnique()
    {
        return new HostId(NewId.NextGuid());
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
