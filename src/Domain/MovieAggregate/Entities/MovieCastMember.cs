namespace ScreenDrafts.Api.Domain.MovieAggregate.Entities;
public sealed class MovieCastMember : Entity<MovieCastMemberId>
{
    private MovieCastMember(
        CastMemberId crewMemberId,
        string roleDescription)
        : base(MovieCastMemberId.CreateUnique())
    {
        CastMemberId = crewMemberId;
        RoleDescription = roleDescription;
    }

    private MovieCastMember()
    {
    }

    public CastMemberId CastMemberId { get; private set; }
    public string? RoleDescription { get; private set; }

    public static MovieCastMember Create(
        CastMemberId crewMemberId,
        string roleDescription)
    {
        return new MovieCastMember(
            crewMemberId,
            roleDescription);
    }
}
