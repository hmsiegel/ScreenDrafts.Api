namespace ScreenDrafts.Api.Presentation.Abstractions;

[Route("api/[controller]")]
[ApiVersionNeutral]
public class VersionNeutralApiController : BaseApiController
{
    protected VersionNeutralApiController(ISender sender, IMapper mapper)
        : base(sender, mapper)
    {
    }
}
