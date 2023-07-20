namespace ScreenDrafts.Api.Domain.CrewMemberAggregate.ValueObjects;
public sealed class CrewMemberId : AggregateRootId<DefaultIdType>
{
    private CrewMemberId(DefaultIdType value)
    {
        Value = value;
    }

    private CrewMemberId()
    {
    }

    public override DefaultIdType Value { get; protected set; }

    public static CrewMemberId Create(DefaultIdType value)
    {
        return new CrewMemberId(value);
    }

    public static CrewMemberId CreateUnique()
    {
        return new CrewMemberId(NewId.NextGuid());
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
