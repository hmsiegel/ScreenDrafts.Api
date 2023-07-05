namespace ScreenDrafts.Api.Presentation.Controllers;

[AllowAnonymous]
public class AuthorizationController : VersionNeutralApiController
{
    private readonly IUserService _userService;

    public AuthorizationController(IUserService userService) => _userService = userService;

    [HttpPost("register")]
    [OpenApiOperation("Register", "Register a new user")]
    [ApiConventionMethod(typeof(ScreenDraftsApiConvention), nameof(ScreenDraftsApiConvention.Register))]
    public Task<string> RegisterAsync([FromBody] RegisterRequest request)
    {
        return _userService.CreateAsync(request, GetOriginFromRequest());
    }

    private string GetOriginFromRequest()
    {
        return $"{Request.Scheme}://{Request.Host.Value}{Request.PathBase.Value}";
    }
}
