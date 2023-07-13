using ScreenDrafts.Api.Application.Drafters.Command.AssignDrafter;

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
}
