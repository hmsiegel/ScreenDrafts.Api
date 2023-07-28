namespace ScreenDrafts.Api.Domain.MovieAggregate.Entities;
public sealed class MovieCastMember : Entity<MovieCastMemberId>
{
    private MovieCastMember(
        CastMemberId castMemberId,
        string roleDescription)
        : base(MovieCastMemberId.CreateUnique())
    {
        CastMemberId = castMemberId;
        RoleDescription = roleDescription;
    }

    private MovieCastMember()
    {
    }

    public CastMemberId CastMemberId { get; private set; }
    public string? RoleDescription { get; private set; }

    public static MovieCastMember Create(
        CastMemberId castMemberId,
        string roleDescription)
    {
        return new MovieCastMember(
            castMemberId,
            roleDescription);
    }
}
