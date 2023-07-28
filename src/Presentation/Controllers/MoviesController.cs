namespace ScreenDrafts.Api.Presentation.Controllers;
public sealed class MoviesController : VersionedApiController
{
    private readonly IImdbService _imdbService;

    public MoviesController(IImdbService imdbService) => _imdbService = imdbService;

    [HttpPost]
    [HasPermission(ScreenDraftsAction.Create, ScreenDraftsResource.Movies)]
    [OpenApiOperation("Create Movie", "Create a movie with the basic information.")]
    public async Task<IActionResult> CreateMovie([FromBody] CreateMovieRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateMovieCommand(
            request.Title,
            request.Year,
            request.ImageUrl,
            request.ImdbUrl);
        var result = await Sender.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost("imdb/{imdbId}")]
    [HasPermission(ScreenDraftsAction.Create, ScreenDraftsResource.Movies)]
    [OpenApiOperation("Create Movie from IMDB", "Create a movie from IMDB.")]
    public async Task<IActionResult> CreateMovieFromImdb(string imdbId, CancellationToken cancellationToken)
    {
        var movie = await _imdbService.GetMovieInformation(imdbId);

        var command = new CreateMovieFromImdbCommand(movie);
        await Sender.Send(command, cancellationToken);

        var castCommand = new CreateMovieCastFromImdbIdCommand(movie);
        await Sender.Send(castCommand, cancellationToken);

        var direcotrsCommand = new CreateMovieDirectorsFromImdbIdCommand(movie);
        await Sender.Send(direcotrsCommand, cancellationToken);

        var writersCommand = new CreateMovieWritersFromImdbIdCommand(movie);
        await Sender.Send(writersCommand, cancellationToken);

        movie = await _imdbService.GetMovieInformation(imdbId, TitleOptions.FullCast);

        var producersCommand = new CreateMovieProducersFromImdbIdCommand(movie);
        var result = await Sender.Send(producersCommand, cancellationToken);

        return Ok(result);
    }

    [HttpPost("crew/{movieId}")]
    [HasPermission(ScreenDraftsAction.Update, ScreenDraftsResource.Movies)]
    [OpenApiOperation("Add Crew Member", "Add a crew member to a movie.")]
    public async Task<IActionResult> AddCrewMember(
        [FromRoute] string movieId,
        [FromBody] AddCrewMemberRequest request,
        CancellationToken cancellationToken)
    {
        var command = Mapper.Map<AddCrewMemberCommand>((movieId, request));
        var result = await Sender.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost("cast/{movieId}")]
    [HasPermission(ScreenDraftsAction.Update, ScreenDraftsResource.Movies)]
    [OpenApiOperation("Add Cast Member", "Add a cast member to a movie.")]
    public async Task<IActionResult> AddCastMember(
        [FromRoute] string movieId,
        [FromBody] AddCastMemberRequest request,
        CancellationToken cancellationToken)
    {
        var command = Mapper.Map<AddCastMemberCommand>((movieId, request));
        var result = await Sender.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [HasPermission(ScreenDraftsAction.View, ScreenDraftsResource.Movies)]
    [OpenApiOperation("Get all Movies", "Get all movies.")]
    public async Task<IActionResult> GetAllMovies(CancellationToken cancellationToken = default)
    {
        var query = new GetAllMoviesQuery();
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [HasPermission(ScreenDraftsAction.View, ScreenDraftsResource.Movies)]
    [OpenApiOperation("Get By Id", "Get by movie Id")]
    public async Task<IActionResult> GetByMovieId(DefaultIdType id, CancellationToken cancellationToken)
    {
        var query = new GetByMovieIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{imdbid}")]
    [HasPermission(ScreenDraftsAction.View, ScreenDraftsResource.Movies)]
    [OpenApiOperation("Get By ImdbId", "Get a movie by the IMDB Id")]
    public async Task<IActionResult> GetByImdbId(string imdbid, CancellationToken cancellationToken)
    {
        var query = new GetMovieByImdbIdQuery(imdbid);
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }
}
