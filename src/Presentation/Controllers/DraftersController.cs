using ScreenDrafts.Api.Application.Drafters.Command.UpdateDrafter;
using ScreenDrafts.Api.Contracts.Drafters;

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

    [HttpPatch("{id}")]
    [HasPermission(ScreenDraftsAction.Update, ScreenDraftsResource.Drafters)]
    [OpenApiOperation("Update a drafter", "Update a drafters' rollover veto and rollover veto override")]
    public async Task<IActionResult> UpdateDrafter(
        [FromRoute] Guid id,
        [FromBody] UpdateDrafterRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateDrafterCommand(id, request.HasRolloverVeto, request.HasRolloverVetoOverride);
        var result = await Sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return HandleFailure(result);
        }

        return Ok(result);
    }
}
