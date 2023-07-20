namespace ScreenDrafts.Api.Domain.DraftAggregate.Enums;
public sealed class Decision : SmartEnum<Decision>
{
    public static readonly Decision Pick = new(nameof(Pick).ToLowerInvariant(), 1);
    public static readonly Decision Veto = new(nameof(Veto).ToLowerInvariant(), 2);
    public static readonly Decision VetoOverride = new(nameof(VetoOverride).ToLowerInvariant(), 3);
    public static readonly Decision CommissionerOverride = new(nameof(CommissionerOverride).ToLowerInvariant(), 4);

    public Decision(string name, int value)
        : base(name, value)
    {
    }
}
