namespace ScreenDrafts.Api.Application.Movies.Queries.GetByMovieId;
internal sealed class GetByMovieIdQueryHandler : IQueryHandler<GetByMovieIdQuery, MovieResponse>
{
    private readonly IMovieRepository _movieRepository;
    private readonly ICastAndCrewService _castAndCrewService;

    public GetByMovieIdQueryHandler(
        IMovieRepository movieRepository,
        ICastAndCrewService castAndCrewService)
    {
        _movieRepository = movieRepository;
        _castAndCrewService = castAndCrewService;
    }

    public async Task<Result<MovieResponse>> Handle(GetByMovieIdQuery request, CancellationToken cancellationToken)
    {
        var movies = await _movieRepository.GetAll(cancellationToken);

        var movie = movies.SingleOrDefault(x => x.Id!.Value == request.Id);

        if (movie is null)
        {
            return Result.Failure<MovieResponse>(DomainErrors.Movie.NotFound);
        }

        var response = new MovieResponse(
            movie.Id!.Value,
            movie.Title!,
            movie.Year!,
            movie.ImageUrl!,
            movie.ImdbUrl!,
            _castAndCrewService.GetCrewMembers(movie),
            _castAndCrewService.GetCastMembers(movie));

        return Result.Success(response);
    }
}
