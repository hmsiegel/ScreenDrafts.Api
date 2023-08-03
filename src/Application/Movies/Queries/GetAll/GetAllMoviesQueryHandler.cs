namespace ScreenDrafts.Api.Application.Movies.Queries.GetAll;
internal sealed class GetAllMoviesQueryHandler : IQueryHandler<GetAllMoviesQuery, List<MovieResponse>>
{
    private readonly IMovieRepository _movieRepository;
    private readonly ICastAndCrewService _castAndCrewService;

    public GetAllMoviesQueryHandler(
        IMovieRepository movieRepository,
        ICastAndCrewService castAndCrewService)
    {
        _movieRepository = movieRepository;
        _castAndCrewService = castAndCrewService;
    }

    public async Task<Result<List<MovieResponse>>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
    {
        var movies = await _movieRepository.GetAll();

        var response = movies.ConvertAll(movie => new MovieResponse(
            movie.Id!.Value,
            movie.Title!,
            movie.Year!,
            movie.ImageUrl!,
            movie.ImdbUrl!,
            _castAndCrewService.GetCrewMembers(movie),
            _castAndCrewService.GetCastMembers(movie)));

        return Result.Success(response);
    }
}
