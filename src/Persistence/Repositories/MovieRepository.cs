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

    public async Task AddCastMemberAsync(Movie movie, CastMember castMember, string roleDescription)
    {
        var movieToUpdate = await _dbContext.Movies
            .Where(x => x.Id == movie.Id)
            .Include(movie => movie.Cast)
            .FirstOrDefaultAsync();

        movieToUpdate!.AddActor(castMember.Id!, roleDescription);
    }

    public async Task AddCrewMemberAsync(Movie movie, CrewMember crewMember, string jobDescription)
    {
        Movie? movieToUpdate;
        switch (CrewType.FromName(jobDescription).Name.ToLower())
        {
            case "director":
                movieToUpdate = await _dbContext.Movies
                    .Where(x => x.Id == movie.Id)
                    .Include(movie => movie.Directors)
                    .FirstOrDefaultAsync();

                movieToUpdate!.AddDirector(crewMember.Id!, jobDescription.ToLower());
                break;

            case "writer":
                movieToUpdate = await _dbContext.Movies
                    .Where(x => x.Id == movie.Id)
                    .Include(movie => movie.Writers)
                    .FirstOrDefaultAsync();

                movieToUpdate!.AddWriter(crewMember.Id!, jobDescription.ToLower());
                break;

            case "producer":
                movieToUpdate = await _dbContext.Movies
                     .Where(x => x.Id == movie.Id)
                     .Include(movie => movie.Producers)
                     .FirstOrDefaultAsync();

                movieToUpdate!.AddProducer(crewMember.Id!, jobDescription.ToLower());
                break;

            default:
                break;
        }
    }

    public async Task<List<Movie>> GetAll() =>
        await _dbContext.Movies
            .ToListAsync();

    public async Task<Movie> GetByIdAsync(DefaultIdType id)
    {
        var movies = await GetAll();
        return movies.SingleOrDefault(x => x.Id!.Value == id);
    }

    public async Task<List<MovieCastMember>> GetAllMovieCastMembers(DefaultIdType id)
    {
        var movies = await GetAll();
        var castList = movies.Where(movie => movie.Id!.Value == id)
            .SelectMany(movie => movie.Cast);
        return castList.ToList();
    }

    public async Task<List<MovieCrewMember>> GetAllMovieCrewMembers(DefaultIdType id)
    {
        var movies = await GetAll();
        var writers = movies.Where(movie => movie.Id!.Value == id)
            .SelectMany(movie => movie.Writers);
        var producers = movies.Where(movie => movie.Id!.Value == id)
            .SelectMany(movie => movie.Producers);
        var directors = movies.Where(movie => movie.Id!.Value == id)
            .SelectMany(movie => movie.Directors);

        var crewList = writers.Concat(producers).Concat(directors);

        return crewList.ToList();
    }
}
