namespace ScreenDrafts.Api.Presentation.Controllers;
public sealed class DraftersController : VersionedApiController
{
    [HttpPost]
    [HasPermission(ScreenDraftsAction.Create, ScreenDraftsResource.Drafters)]
    [OpenApiOperation("Create Drafter", "Make a user a drafter.")]
    public async Task<IActionResult> AssignDrafter(
        [FromBody] AssignDrafterCommand request,
        CancellationToken cancellationToken = default)
    {
        var command = Mapper.Map<AssignDrafterCommand>(request);
        var result = await Sender.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [HasPermission(ScreenDraftsAction.View, ScreenDraftsResource.Drafters)]
    [OpenApiOperation("Get Drafter", "Get a drafter by id.")]
    public async Task<IActionResult> GetDrafter(
        [FromRoute] string id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [HasPermission(ScreenDraftsAction.View, ScreenDraftsResource.Drafters)]
    [OpenApiOperation("Get Drafters", "Get all drafters.")]
    public async Task<IActionResult> GetDrafters(
               CancellationToken cancellationToken = default)
    {
        var query = new GetAllQuery();
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }
}
