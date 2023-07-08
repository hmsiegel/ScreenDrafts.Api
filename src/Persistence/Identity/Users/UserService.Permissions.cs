namespace ScreenDrafts.Api.Persistence.Identity.Users;
internal sealed partial class UserService
{
    public async Task<bool> HasPermissionAsync(string userId, string permission, CancellationToken cancellation = default)
    {
        var permissions = await _cache.GetOrSetAsync(
            _cacheKey.GetCacheKey(ScreenDraftsClaims.Permission, userId),
            () => GetPermissionsAsync(userId, cancellation),
            TimeSpan.FromMinutes(5),
            cancellation);

        return permissions?.Contains(permission) ?? false;
    }

    public Task InvalidatePermissionCacheAsync(string userId, CancellationToken cancellationToken) =>
        _cache.RemoveAsync(_cacheKey.GetCacheKey(ScreenDraftsClaims.Permission, userId), cancellationToken);

    public async Task<List<string>> GetPermissionsAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId);

        _ = user ?? throw new UnauthorizedException("Authentication failed");

        var userRoles = await _userManager.GetRolesAsync(user);
        var permissions = new List<string>();
        foreach (var role in await _roleManager.Roles
            .Where(r => userRoles.Contains(r.Name!))
            .ToListAsync(cancellationToken))
        {
            permissions.AddRange(await _dbContext.RoleClaims
                               .Where(rc => rc.RoleId == role.Id && rc.ClaimType == ScreenDraftsClaims.Permission)
                               .Select(rc => rc.ClaimValue!)
                               .ToListAsync(cancellationToken));
        }

        return permissions.Distinct().ToList();
    }
}
