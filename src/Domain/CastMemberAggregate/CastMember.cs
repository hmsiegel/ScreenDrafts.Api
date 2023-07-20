namespace ScreenDrafts.Api.Domain.CastMemberAggregate;
public sealed class CastMember : AggregateRoot<CastMemberId, DefaultIdType>, ICastAndCrew, IAuditableEntity
{
    private CastMember(
        CastMemberId id,
        string? imdbId,
        string? name)
        : base(id)
    {
        ImdbId = imdbId;
        Name = name;
    }

    private CastMember()
    {
    }

    public string? ImdbId { get; private set; }
    public string? Name { get; private set; }

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DefaultIdType? ModifiedBy { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static CastMember Create(
        string imdbId,
        string name)
    {
        return new(
            CastMemberId.CreateUnique(),
            imdbId,
            name);
    }
}
