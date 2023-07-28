namespace ScreenDrafts.Api.Domain.Repositories;
public interface IMovieRepository
{
    void Add(Movie movie);
    Task AddCrewMemberAsync(Movie movie, CrewMember crewMember, string jobDescription);
    Task AddCastMemberAsync(Movie movie, CastMember castMember, string roleDescription);
    Task<Movie> GetByIdAsync(DefaultIdType id);
    Task<List<Movie>> GetAll();
    Task<List<MovieCastMember>> GetAllMovieCastMembers(DefaultIdType id);
    Task<List<MovieCrewMember>> GetAllMovieCrewMembers(DefaultIdType id);
}
