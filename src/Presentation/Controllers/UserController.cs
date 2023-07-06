﻿namespace ScreenDrafts.Api.Presentation.Controllers;
public sealed class UserController : VersionNeutralApiController
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [OpenApiOperation("Get a list of all users", "")]
    public Task<List<UserDetailsResponse>> GetListAsync(CancellationToken cancellationToken)
    {
        return _userService.GetListAsync(cancellationToken);
    }

    [HttpGet("{id}")]
    [OpenApiOperation("Get a user's details", "")]
    public Task<UserDetailsResponse> GetAsync(string id, CancellationToken cancellationToken)
    {
        return _userService.GetAsync(id, cancellationToken);
    }

    [HttpGet("{id}/roles")]
    [OpenApiOperation("Get a user's roles", "")]
    public Task<List<UserRoleResponse>> GetRolesAsync(string id, CancellationToken cancellationToken)
    {
        return _userService.GetRolesAsync(id, cancellationToken);
    }

    [HttpPost("{id}/roles")]
    [ApiConventionMethod(typeof(ScreenDraftsApiConvention), nameof(ScreenDraftsApiConvention.Register))]
    [OpenApiOperation("Update a user's assigned roles", "")]
    public Task<string> AssignRolesAsync(string id, [FromBody]UserRolesRequest request, CancellationToken cancellationToken)
    {
        return _userService.AssignRolesAsync(id, request, cancellationToken);
    }

    [ HttpPost("{id}/toggle-status")]
    [ApiConventionMethod(typeof(ScreenDraftsApiConvention), nameof(ScreenDraftsApiConvention.Register))]
    [OpenApiOperation("Toggle a user's active status", "")]
    public async Task<IActionResult> ToggleStatusAsync(string id, [FromBody]ToggleUserStatusRequest request, CancellationToken cancellationToken)
    {
        if (id != request.UserId)
        {
            return BadRequest();
        }

        await _userService.ToggleStatusAsync(request, cancellationToken);

        return Ok();
    }

    [HttpGet("confim-email")]
    [AllowAnonymous]
    [OpenApiOperation("Confirm a user's email address", "")]
    [ApiConventionMethod(typeof(ScreenDraftsApiConvention), nameof(ScreenDraftsApiConvention.Search))]
    public Task<string> ConfirmEmailAsync([FromQuery]string userId, [FromQuery]string code, CancellationToken cancellationToken)
    {
        return _userService.ConfirmEmailAsync(userId, code, cancellationToken);
    }

    [HttpGet("confirm-phone-number")]
    [AllowAnonymous]
    [OpenApiOperation("Confirm a user's phone number", "")]
    [ApiConventionMethod(typeof(ScreenDraftsApiConvention), nameof(ScreenDraftsApiConvention.Search))]
    public Task<string> ConfirmPhoneNumber([FromQuery]string userId, [FromQuery]string code)
    {
        return _userService.ConfirmPhoneNumberAsync(userId, code);
    }
}
