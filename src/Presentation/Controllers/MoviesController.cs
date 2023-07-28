namespace ScreenDrafts.Api.Presentation.Controllers;
public sealed class MoviesController : VersionedApiController
{
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
