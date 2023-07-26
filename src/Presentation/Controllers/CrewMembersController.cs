namespace ScreenDrafts.Api.Presentation.Controllers;
public sealed class CrewMembersController : VersionedApiController
{
    [HttpPost]
    [HasPermission(ScreenDraftsAction.Create, ScreenDraftsResource.CrewMembers)]
    [OpenApiOperation("Create Crew Member", "Create a crew member with the basic information.")]
    public async Task<IActionResult> CreateCrewMember([FromBody] CreateCrewMemberRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCrewMemberCommand(
            request.ImdbId,
            request.Name);
        var result = await Sender.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [HasPermission(ScreenDraftsAction.View, ScreenDraftsResource.CrewMembers)]
    [OpenApiOperation("Get Crew Member", "Get a cast member by id.")]
    public async Task<IActionResult> GetCrewMember(
        [FromRoute] string id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetCrewMemberByIdQuery(DefaultIdType.Parse(id));
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [HasPermission(ScreenDraftsAction.View, ScreenDraftsResource.CrewMembers)]
    [OpenApiOperation("Get All Crew Members", "Get all cast members.")]
    public async Task<IActionResult> GetCrewMembers(
               CancellationToken cancellationToken = default)
    {
        var query = new GetAllCrewMembersQuery();
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }
}
