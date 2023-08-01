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

    [HttpPost("{draftId}/drafter")]
    [HasPermission(ScreenDraftsAction.Update, ScreenDraftsResource.Drafts)]
    [OpenApiOperation("Update Draft", "Add a drafter to a draft.")]
    public async Task<IActionResult> AddDrafterToDraft(
        [FromRoute] DefaultIdType draftId,
        [FromBody] AddDrafterRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new AddDrafterCommand(draftId, request.UserId);
        var result = await Sender.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost("{draftId}/host")]
    [HasPermission(ScreenDraftsAction.Update, ScreenDraftsResource.Drafts)]
    [OpenApiOperation("Update Draft", "Add a host to a draft")]
    public async Task<IActionResult> AddHostToDraft(
        [FromRoute] DefaultIdType draftId,
        [FromBody] AddHostsRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new AddHostCommand(draftId, request.UserId);
        var result = await Sender.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [HasPermission(ScreenDraftsAction.View, ScreenDraftsResource.Drafts)]
    [OpenApiOperation("Get Drafts", "Get a draft by id.")]
    public async Task<IActionResult> GetDrafts(
        [FromRoute] string id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetDraftByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("basic")]
    [HasPermission(ScreenDraftsAction.View, ScreenDraftsResource.Drafts)]
    [OpenApiOperation("Get drafts", "Get all drafts with basic information.")]
    public async Task<IActionResult> GetAllDrafts(CancellationToken cancellationToken = default)
    {
        var query = new GetAllDraftsBasicQuery();
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [HasPermission(ScreenDraftsAction.View, ScreenDraftsResource.Drafts)]
    [OpenApiOperation("Get drafts", "Get all drafts with draftes and hosts.")]
    public async Task<IActionResult> GetAllDraftsWithDraftersAndHosts(CancellationToken cancellationToken = default)
    {
        var query = new GetAllDraftsWithDraftersAndHostsQuery();
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("advanced")]
    [HasPermission(ScreenDraftsAction.View, ScreenDraftsResource.Drafts)]
    [OpenApiOperation("Get drafts", "Get all drafts with all information")]
    public async Task<IActionResult> GetAllDraftsFull(CancellationToken cancellationToken = default)
    {
        var query = new GetAllDraftsQuery();
        var result = await Sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [HasPermission(ScreenDraftsAction.Update, ScreenDraftsResource.Drafts)]
    [OpenApiOperation("Update Draft", "Update a draft.")]
    public async Task<IActionResult> UpdateDraft(
        [FromRoute] string id,
        [FromBody] UpdateDraftRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = Mapper.Map<UpdateDraftCommand>((id, request));
        var result = await Sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpPut("{draftId}/pick")]
    [HasPermission(ScreenDraftsAction.Update, ScreenDraftsResource.Drafts)]
    [OpenApiOperation("Add Draft Pick", "Add a draft pick to the draft.")]
    public async Task<IActionResult> AddDraftPic(
        [FromRoute] string draftId,
        [FromBody] AddDraftPickRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = Mapper.Map<AddDraftPickCommand>((draftId, request));
        var result = await Sender.Send(command, cancellationToken);

        return Ok(result);
    }
}
