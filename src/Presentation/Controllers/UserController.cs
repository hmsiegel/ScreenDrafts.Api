namespace ScreenDrafts.Api.Presentation.Controllers;
public sealed class UserController : VersionNeutralApiController
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [OpenApiOperation("Get Users", "Get a list of users")]
    public Task<List<UserDetailsResponse>> GetListAsync(CancellationToken cancellationToken)
    {
        return _userService.GetListAsync(cancellationToken);
    }

    [HttpGet("{id}")]
    [OpenApiOperation("Get User", "Get a user by id")]
    public Task<UserDetailsResponse> GetAsync(string id, CancellationToken cancellationToken)
    {
        return _userService.GetAsync(id, cancellationToken);
    }
}
