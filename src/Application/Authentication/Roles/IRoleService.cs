namespace ScreenDrafts.Api.Application.Authentication.Roles;
public interface IRoleService : ITransientService
{
    Task<List<RoleResponse>> GetListAsync(CancellationToken cancellationToken);

    Task<bool> ExistsAsync(string roleName, string? excludeId);

    Task<RoleResponse> GetByIdAsync(string id);

    Task<RoleResponse> GetByIdWithPermissionsAsync(string roleId, CancellationToken cancellationToken);

    Task<RoleResponse> GetByNameAsync(string roleName, CancellationToken cancellationToken);

    Task<string> CreateOrUpdateAsync(CreateOrUpdateRoleRequest request);

    Task<string> UpdatePermissionsAsync(UpdateRolePermissionsRequest request, CancellationToken cancellationToken);

    Task<string> DeleteAsync(string id);
}
