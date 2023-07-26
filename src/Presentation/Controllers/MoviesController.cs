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

    [HttpPost("crew")]
    [HasPermission(ScreenDraftsAction.Update, ScreenDraftsResource.Movies)]
    [OpenApiOperation("Add Crew Member", "Add a crew member to a movie.")]
    public Task<IActionResult> AddCrewMember([FromBody] AddCrewMemberRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
