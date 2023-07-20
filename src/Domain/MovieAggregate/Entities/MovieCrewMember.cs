namespace ScreenDrafts.Api.Domain.MovieAggregate.Entities;
public sealed class MovieCrewMember : Entity<MovieCrewMemberId>
{
    private MovieCrewMember(
        CrewMemberId crewMemberId,
        string jobDescription)
        : base(MovieCrewMemberId.CreateUnique())
    {
        CrewMemberId = crewMemberId;
        JobDescription = jobDescription;
    }

    private MovieCrewMember()
    {
    }

    public CrewMemberId CrewMemberId { get; private set; }
    public string? JobDescription { get; private set; }

    public static MovieCrewMember Create(
        CrewMemberId crewMemberId,
        string jobDescription)
    {
        return new MovieCrewMember(
            crewMemberId,
            jobDescription);
    }
}
