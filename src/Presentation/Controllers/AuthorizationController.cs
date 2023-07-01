namespace ScreenDrafts.Api.Presentation.Controllers;

[AllowAnonymous]
public class AuthorizationController : VersionNeutralApiController
{
    [HttpPost("register")]
    public Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
    {
        throw new NotImplementedException("Not implemented");
    }
}
