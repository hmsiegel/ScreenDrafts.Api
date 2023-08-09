namespace ScreenDrafts.Api.Domain.Repositories;
public interface IMovieRepository
{
    void Add(Movie movie);
    Task AddCrewMemberAsync(Movie movie, CrewMember crewMember, string jobDescription, CancellationToken cancellationToken = default);
    Task AddCastMemberAsync(Movie movie, CastMember castMember, string roleDescription, CancellationToken cancellationToken = default);
    Task<Movie> GetByIdAsync(DefaultIdType id, CancellationToken cancellationToken = default);
    Task<Movie> GetByImdbIdAsync(string imdbId, CancellationToken cancellationToken = default);
    Task<List<Movie>> GetAll(CancellationToken cancellationToken = default);
    Task<List<MovieCastMember>> GetAllMovieCastMembers(DefaultIdType id, CancellationToken cancellationToken = default);
    Task<List<MovieCrewMember>> GetAllMovieCrewMembers(DefaultIdType id, CancellationToken cancellationToken = default);
    Task<bool> DoesMovieExist(string title, string year, CancellationToken cancellationToken = default);
    Task<List<Movie>> GetMoviesWithCastMember(DefaultIdType Id, CancellationToken cancellationToken = default);
    Task<List<Movie>> GetMoviesByYearAsync(string year, CancellationToken cancellationToken = default);
}
