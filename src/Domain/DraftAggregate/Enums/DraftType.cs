namespace ScreenDrafts.Api.Domain.DraftAggregate.Enums;

public class DraftType : SmartEnum<DraftType>
{
    public static readonly DraftType Regular = new(nameof(Regular).ToLowerInvariant(), 1);
    public static readonly DraftType MiniMega = new(nameof(MiniMega).ToLowerInvariant(), 2);
    public static readonly DraftType Mega = new(nameof(Mega).ToLowerInvariant(), 3);
    public static readonly DraftType Super = new(nameof(Super).ToLowerInvariant(), 4);
    public static readonly DraftType MiniSuper = new(nameof(MiniSuper).ToLowerInvariant(), 5);

    public DraftType(string name, int value)
        : base(name, value)
    {
    }
}
