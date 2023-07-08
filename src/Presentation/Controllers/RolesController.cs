namespace ScreenDrafts.Api.Presentation.Controllers;
public sealed class RolesController : VersionNeutralApiController
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService) => _roleService = roleService;

    [HttpGet]
    [HasPermission(ScreenDraftsAction.View, ScreenDraftsResource.Roles)]
    [OpenApiOperation("Get a list of all roles", "")]
    public Task<List<RoleResponse>> GetListAsync(CancellationToken cancellationToken)
    {
        return _roleService.GetListAsync(cancellationToken);
    }

    [HttpGet("{id}")]
    [OpenApiOperation("Get a role by id", "")]
    [HasPermission(ScreenDraftsAction.View, ScreenDraftsResource.Roles)]
    public Task<RoleResponse> GetByIdAsync(string id)
    {
        return _roleService.GetByIdAsync(id);
    }

    [HttpGet("{id}/permissions")]
    [OpenApiOperation("Get role details with it's permissions", "")]
    [HasPermission(ScreenDraftsAction.View, ScreenDraftsResource.RoleClaims)]
    public Task<RoleResponse> GetByIdWithPermissionsAsync(string id, CancellationToken cancellationToken)
    {
        return _roleService.GetByIdWithPermissionsAsync(id, cancellationToken);
    }

    [HttpPut("{id}/permissions")]
    [OpenApiOperation("Update a role's permissions", "")]
    [HasPermission(ScreenDraftsAction.Update, ScreenDraftsResource.RoleClaims)]
    public async Task<ActionResult<string>> UpdatePermissionsAsync(string id, UpdateRolePermissionsRequest request, CancellationToken cancellationToken)
    {
        if (id != request.RoleId)
        {
            return BadRequest();
        }

        return Ok(await _roleService.UpdatePermissionsAsync(request, cancellationToken));
    }

    [HttpPost]
    [OpenApiOperation("Create or update a role.", "")]
    [HasPermission(ScreenDraftsAction.Create, ScreenDraftsResource.Roles)]
    public Task<string> RegisterRoleAsync(CreateOrUpdateRoleRequest request)
    {
        return _roleService.CreateOrUpdateAsync(request);
    }

    [HttpDelete("{id}")]
    [OpenApiOperation("Delete a role", "")]
    [HasPermission(ScreenDraftsAction.Delete, ScreenDraftsResource.Roles)]
    public Task<string> DeleteAsync(string id)
    {
        return _roleService.DeleteAsync(id);
    }
}
