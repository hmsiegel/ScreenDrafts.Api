namespace ScreenDrafts.Api.Presentation.Controllers;
public sealed class CastMembersController : VersionedApiController
{
    [HttpPost]
    [HasPermission(ScreenDraftsAction.Create, ScreenDraftsResource.CastMembers)]
    [OpenApiOperation("Create Cast Member", "Create a crew member with the basic information.")]

    public async Task<IActionResult> CreateCastMember([FromBody] CreateCastMemberRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCastMemberCommand(
            request.ImdbId,
            request.Name);
        var result = await Sender.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [HasPermission(ScreenDraftsAction.View, ScreenDraftsResource.CastMembers)]
    [OpenApiOperation("Get Cast Member", "Get a cast member by id.")]
    public async Task<IActionResult> GetCastMember(
        [FromRoute] string id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetCastMemberByIdQuery(DefaultIdType.Parse(id));
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [HasPermission(ScreenDraftsAction.View, ScreenDraftsResource.CastMembers)]
    [OpenApiOperation("Get All Cast Members", "Get all cast members.")]
    public async Task<IActionResult> GetCastMembers(
               CancellationToken cancellationToken = default)
    {
        var query = new GetAllCastMembersQuery();
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }
}
