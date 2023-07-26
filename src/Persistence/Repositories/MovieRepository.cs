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
}
