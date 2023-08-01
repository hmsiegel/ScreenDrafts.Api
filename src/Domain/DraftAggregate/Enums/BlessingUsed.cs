namespace ScreenDrafts.Api.Domain.DraftAggregate.Enums;
public sealed class BlessingUsed : SmartEnum<BlessingUsed>
{
    public static readonly BlessingUsed Veto = new(nameof(Veto).ToLowerInvariant(), 1);
    public static readonly BlessingUsed VetoOverride = new(nameof(VetoOverride).ToLowerInvariant(), 2);
    public static readonly BlessingUsed CommissionerOverride = new(nameof(CommissionerOverride).ToLowerInvariant(), 3);

    public BlessingUsed(string name, int value)
        : base(name, value)
    {
    }
}
