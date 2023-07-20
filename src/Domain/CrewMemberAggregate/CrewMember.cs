namespace ScreenDrafts.Api.Domain.CrewMemberAggregate;
public sealed class CrewMember : AggregateRoot<CrewMemberId, DefaultIdType>, ICastAndCrew, IAuditableEntity
{
    private CrewMember(
        CrewMemberId id,
        string? imdbId,
        string? name)
        : base(id)
    {
        ImdbId = imdbId;
        Name = name;
    }

    private CrewMember()
    {
    }

    public string? ImdbId { get; private set; }
    public string? Name { get; private set; }

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DefaultIdType? ModifiedBy { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static CrewMember Create(
        string imdbId,
        string name)
    {
        return new(
            CrewMemberId.CreateUnique(),
            imdbId,
            name);
    }
}
