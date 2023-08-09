namespace ScreenDrafts.Api.Persistence.Repositories;
public sealed class MovieRepository : IMovieRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MovieRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Movie movie)
    {
        _dbContext.Movies.Add(movie);
    }

    public async Task AddCastMemberAsync(Movie movie, CastMember castMember, string roleDescription, CancellationToken cancellationToken = default)
    {
        var movieToUpdate = await _dbContext.Movies
            .Where(x => x.Id == movie.Id)
            .Include(movie => movie.Cast)
            .FirstOrDefaultAsync(cancellationToken);

        movieToUpdate!.AddActor(castMember.Id!, roleDescription);
    }

    public async Task AddCrewMemberAsync(Movie movie, CrewMember crewMember, string jobDescription, CancellationToken cancellationToken = default)
    {
        Movie? movieToUpdate;
        switch (CrewType.FromName(jobDescription).Name.ToLower())
        {
            case "director":
                movieToUpdate = await _dbContext.Movies
                    .Where(x => x.Id == movie.Id)
                    .Include(movie => movie.Directors)
                    .FirstOrDefaultAsync(cancellationToken);

                movieToUpdate!.AddDirector(crewMember.Id!, jobDescription.ToLower());
                break;

            case "writer":
                movieToUpdate = await _dbContext.Movies
                    .Where(x => x.Id == movie.Id)
                    .Include(movie => movie.Writers)
                    .FirstOrDefaultAsync(cancellationToken);

                movieToUpdate!.AddWriter(crewMember.Id!, jobDescription.ToLower());
                break;

            case "producer":
                movieToUpdate = await _dbContext.Movies
                     .Where(x => x.Id == movie.Id)
                     .Include(movie => movie.Producers)
                     .FirstOrDefaultAsync(cancellationToken);

                movieToUpdate!.AddProducer(crewMember.Id!, jobDescription.ToLower());
                break;

            default:
                break;
        }
    }

    public async Task<List<Movie>> GetAll(CancellationToken cancellationToken = default) =>
        await _dbContext.Movies
            .ToListAsync(cancellationToken);

    public async Task<Movie> GetByIdAsync(DefaultIdType id, CancellationToken cancellationToken = default)
    {
        var movies = await GetAll(cancellationToken);
        return movies.SingleOrDefault(x => x.Id!.Value == id);
    }

    public async Task<List<MovieCastMember>> GetAllMovieCastMembers(DefaultIdType id, CancellationToken cancellationToken = default)
    {
        var movies = await GetAll(cancellationToken);
        var castList = movies.Where(movie => movie.Id!.Value == id)
            .SelectMany(movie => movie.Cast);
        return castList.ToList();
    }

    public async Task<List<MovieCrewMember>> GetAllMovieCrewMembers(DefaultIdType id, CancellationToken cancellationToken = default)
    {
        var movies = await GetAll(cancellationToken);
        var writers = movies.Where(movie => movie.Id!.Value == id)
            .SelectMany(movie => movie.Writers);
        var producers = movies.Where(movie => movie.Id!.Value == id)
            .SelectMany(movie => movie.Producers);
        var directors = movies.Where(movie => movie.Id!.Value == id)
            .SelectMany(movie => movie.Directors);

        var crewList = writers.Concat(producers).Concat(directors);

        return crewList.ToList();
    }

    public async Task<Movie> GetByImdbIdAsync(string imdbId, CancellationToken cancellationToken = default)
    {
        var movies = await GetAll(cancellationToken);
        return movies.SingleOrDefault(x => new Uri(x.ImdbUrl!).Segments.Last() == imdbId);
    }

    public async Task<bool> DoesMovieExist(string title, string year, CancellationToken cancellationToken = default)
    {
        var movies = await GetAll(cancellationToken);
        return movies.Exists(x => x.Title == title && x.Year == year);
    }

    public async Task<List<Movie>> GetMoviesWithCastMember(DefaultIdType Id, CancellationToken cancellationToken = default)
    {
        var movies = await _dbContext.Movies
            .Include(x => x.Cast)
            .ToListAsync(cancellationToken);

        var moviesWithCastMember = movies
            .Where(movie => movie.Cast.Any(castMember => castMember.CastMemberId.Value == Id))
            .ToList();

        return moviesWithCastMember;
    }

    public async Task<List<Movie>> GetMoviesByYearAsync(string year, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Movies
            .Where(x => x.Year == year)
            .ToListAsync(cancellationToken);
    }
}
