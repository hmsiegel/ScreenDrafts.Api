using ScreenDrafts.Api.Contracts.Drafts;

namespace ScreenDrafts.Api.Presentation.Controllers;
public class DraftsController : VersionedApiController
{
    [HttpPost]
    [HasPermission(ScreenDraftsAction.Create, ScreenDraftsResource.Drafts)]
    [OpenApiOperation("Create Draft", "Create a draft with the basic information.")]
    public async Task<IActionResult> CreateDraft(
        [FromBody] CreateDraftRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = Mapper.Map<CreateDraftCommand>(request);
        var result = await Sender.Send(command, cancellationToken);
        return Ok(result);
    }
}
