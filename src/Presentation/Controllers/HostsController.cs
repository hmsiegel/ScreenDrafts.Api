namespace ScreenHosts.Api.Presentation.Controllers;
public sealed class HostsController : VersionedApiController
{
    [HttpPost]
    [HasPermission(ScreenDraftsAction.Create, ScreenDraftsResource.Hosts)]
    [OpenApiOperation("Create Host", "Make a user a drafter.")]
    public async Task<IActionResult> AssignHost(
        [FromBody] AssignHostCommand request,
        CancellationToken cancellationToken = default)
    {
        var command = Mapper.Map<AssignHostCommand>(request);
        var result = await Sender.Send(command, cancellationToken);
        return Ok(result.IsSuccess);
    }

    [HttpGet("{id}")]
    [HasPermission(ScreenDraftsAction.View, ScreenDraftsResource.Hosts)]
    [OpenApiOperation("Get Host", "Get a drafter by id.")]
    public async Task<IActionResult> GetHost(
        [FromRoute] string id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetHostByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [HasPermission(ScreenDraftsAction.View, ScreenDraftsResource.Hosts)]
    [OpenApiOperation("Get Hosts", "Get all drafters.")]
    public async Task<IActionResult> GetHosts(
               CancellationToken cancellationToken = default)
    {
        var query = new GetAllHostsQuery();
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id}")]
    [HasPermission(ScreenDraftsAction.Update, ScreenDraftsResource.Hosts)]
    [OpenApiOperation("Update a drafter", "Update a drafters' rollover veto and rollover veto override")]
    public async Task<IActionResult> UpdateHost(
        [FromRoute] DefaultIdType id,
        [FromBody] UpdateHostRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateHostCommand(id, request.PredictionPoints);
        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }
}
