namespace ScreenDrafts.Api.Domain.MovieAggregate.Enums;
public sealed class CrewType : SmartEnum<CrewType>
{
    public static readonly CrewType Director = new(nameof(Director), 1);
    public static readonly CrewType Writer = new(nameof(Writer), 2);
    public static readonly CrewType Producer = new(nameof(Producer), 3);

    public CrewType(string name, int value)
        : base(name, value)
    {
    }
}
