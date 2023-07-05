namespace ScreenDrafts.Api.Presentation.Controllers;

[AllowAnonymous]
public class AuthorizationController : VersionNeutralApiController
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public AuthorizationController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    [OpenApiOperation("Register", "Register a new user")]
    [ApiConventionMethod(typeof(ScreenDraftsApiConvention), nameof(ScreenDraftsApiConvention.Register))]
    public Task<string> RegisterAsync([FromBody] RegisterRequest request)
    {
        return _userService.CreateAsync(request, GetOriginFromRequest());
    }

    [HttpPost("login")]
    [OpenApiOperation("Login", "Login an existing user")]
    public Task<TokenResponse> LoginAsync([FromBody] TokenRequest request, CancellationToken cancellationToken)
    {
        return _tokenService.GetTokenAsync(request, GetIpAddress()!, cancellationToken);
    }

    private string GetOriginFromRequest()
    {
        return $"{Request.Scheme}://{Request.Host.Value}{Request.PathBase.Value}";
    }

    private string? GetIpAddress() =>
        Request.Headers.ContainsKey("X-Forwarded-For")
            ? Request.Headers["X-Forwarded-For"].ToString()
            : HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "N/A";
}
