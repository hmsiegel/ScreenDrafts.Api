namespace ScreenDrafts.Api.Presentation.Controllers;

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
    [AllowAnonymous]
    [OpenApiOperation("Register", "Register a new user")]
    [ApiConventionMethod(typeof(ScreenDraftsApiConvention), nameof(ScreenDraftsApiConvention.Register))]
    public Task<string> RegisterAsync([FromBody] RegisterRequest request)
    {
        return _userService.CreateAsync(request, GetOriginFromRequest());
    }

    [HttpPost("create-user")]
    [HasPermission(ScreenDraftsAction.Create, ScreenDraftsResource.Users)]
    [OpenApiOperation("Create User", "Create a new user who hasn't registered.")]
    [ApiConventionMethod(typeof(ScreenDraftsApiConvention), nameof(ScreenDraftsApiConvention.Register))]
    public Task<string> CreateUserAsync([FromBody] CreateUserRequest request)
    {
        return _userService.CreateAsync(request, GetOriginFromRequest());
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [OpenApiOperation("Login", "Login an existing user")]
    public Task<TokenResponse> LoginAsync([FromBody] TokenRequest request, CancellationToken cancellationToken)
    {
        return _tokenService.GetTokenAsync(request, GetIpAddress()!, cancellationToken);
    }

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    [OpenApiOperation("Request an access token using a refresh token", "")]
    [ApiConventionMethod(typeof(ScreenDraftsApiConvention), nameof(ScreenDraftsApiConvention.Search))]
    public Task<TokenResponse> RefreshAsync(RefreshTokenRequest request)
    {
        return _tokenService.RefreshTokenAsync(request, GetIpAddress()!);
    }

    [HttpGet("forgot-password")]
    [AllowAnonymous]
    [OpenApiOperation("Request a password reset email for a user", "")]
    [ApiConventionMethod(typeof(ScreenDraftsApiConvention), nameof(ScreenDraftsApiConvention.Register))]
    public Task<string> ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        return _userService.ForgotPasswordAsync(request, GetOriginFromRequest());
    }

    [HttpGet("reset-password")]
    [AllowAnonymous]
    [OpenApiOperation("Reset a user's password", "")]
    [ApiConventionMethod(typeof(ScreenDraftsApiConvention), nameof(ScreenDraftsApiConvention.Register))]
    public Task<string> ResetPasswordAsync(ResetPasswordRequest request)
    {
        return _userService.ResetPasswordAsync(request, GetOriginFromRequest());
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
