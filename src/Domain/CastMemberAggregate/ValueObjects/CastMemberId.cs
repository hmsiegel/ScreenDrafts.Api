namespace ScreenDrafts.Api.Domain.CastMemberAggregate.ValueObjects;
public sealed class CastMemberId : AggregateRootId<DefaultIdType>
{
    private CastMemberId(DefaultIdType value)
    {
        Value = value;
    }

    private CastMemberId()
    {
    }

    public override DefaultIdType Value { get; protected set; }

    public static CastMemberId Create(DefaultIdType value)
    {
        return new CastMemberId(value);
    }

    public static CastMemberId CreateUnique()
    {
        return new CastMemberId(NewId.NextGuid());
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
