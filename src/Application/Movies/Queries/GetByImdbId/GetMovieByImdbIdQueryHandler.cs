namespace ScreenDrafts.Api.Application.Movies.Queries.GetByImdbId;
internal sealed class GetMovieByImdbIdQueryHandler : IQueryHandler<GetMovieByImdbIdQuery, MovieResponse>
{
    private readonly IMovieRepository _movieRepository;
    private readonly ICastAndCrewService _castAndCrewService;

    public GetMovieByImdbIdQueryHandler(
        IMovieRepository movieRepository,
        ICastAndCrewService castAndCrewService)
    {
        _movieRepository = movieRepository;
        _castAndCrewService = castAndCrewService;
    }

    public async Task<Result<MovieResponse>> Handle(GetMovieByImdbIdQuery request, CancellationToken cancellationToken)
    {
        var movies = await _movieRepository.GetAll(cancellationToken);
        var movie = movies.Find(x => new Uri(x.ImdbUrl!).Segments.Last() == request.ImdbId);

        if (movie is null)
        {
            return Result.Failure<MovieResponse>(DomainErrors.Movie.NotFound);
        }

        List<MovieCrewMemberResponse> crewMembers = _castAndCrewService.GetCrewMembers(movie);

        List<MovieCastMemberResponse> castMembers = _castAndCrewService.GetCastMembers(movie);

        var response = new MovieResponse(
            movie.Id!.Value,
            movie.Title!,
            movie.Year!,
            movie.ImageUrl!,
            movie.ImdbUrl!,
            crewMembers,
            castMembers);

        return Result.Success(response);
    }
}
