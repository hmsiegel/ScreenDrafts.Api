namespace ScreenDrafts.Api.Presentation.Abstractions;

[Route("api/v{version:apiVersion}/[controller]")]
public class VersionedApiController : BaseApiController
{
    protected VersionedApiController(ISender sender, IMapper mapper)
        : base(sender, mapper)
    {
    }
}
