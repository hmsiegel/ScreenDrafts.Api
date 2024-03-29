﻿namespace ScreenDrafts.Api.Persistence.Identity;
internal class RoleService : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICurrentUserService _currentUser;
    private readonly ApplicationDbContext _dbContext;
    private readonly IEventPublisher _events;

    public RoleService(
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager,
        ICurrentUserService currentUser,
        ApplicationDbContext dbContext,
        IEventPublisher events)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _currentUser = currentUser;
        _dbContext = dbContext;
        _events = events;
    }

    public async Task<string> CreateOrUpdateAsync(CreateOrUpdateRoleRequest request)
    {
        if (string.IsNullOrEmpty(request.Id))
        {
            // Create new role
            var role = new ApplicationRole(request.Name, request.Description);
            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                throw new InternalServerException("Failed to create role");
            }

            await _events.PublishAsync(new ApplicationRoleCreatedEvent(DefaultIdType.NewGuid(), role.Id, role.Name!));

            return string.Format(string.Format("Role {0} created", request.Name));
        }
        else
        {
            // Update existing role
            var role = await _roleManager.FindByIdAsync(request.Id);
            _ = role ?? throw new NotFoundException("Role not found");

            if (ScreenDraftsRoles.IsDefault(role.Name!))
            {
                throw new ConflictException(string.Format("Not allowed to modify the role {0}.", role.Name));
            }

            role.Name = request.Name;
            role.Description = request.Description;
            role.NormalizedName = request.Name.ToUpperInvariant();
            var result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                throw new InternalServerException("Failed to update role", result.GetErrors());
            }

            await _events.PublishAsync(new ApplicationRoleUpdatedEvent(DefaultIdType.NewGuid(), role.Id, role.Name!));

            return string.Format(string.Format("Role {0} updated", role.Name));
        }
    }

    public async Task<string> DeleteAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        _ = role ?? throw new NotFoundException("Role not found");

        if (ScreenDraftsRoles.IsDefault(role.Name!))
        {
            throw new ConflictException(string.Format("Not allowed to delete the role {0}.", role.Name));
        }

        if ((await _userManager.GetUsersInRoleAsync(role.Name!)).Count > 0)
        {
            throw new ConflictException(string.Format("Not allowed to delete the role {0} because it is assigned to one or more users.", role.Name));
        }

        await _roleManager.DeleteAsync(role);

        await _events.PublishAsync(new ApplicationRoleDeletedEvent(DefaultIdType.NewGuid(), role.Id, role.Name!, false));

        return string.Format(string.Format("Role {0} deleted", role.Name));
    }

    public async Task<bool> ExistsAsync(string roleName, string? excludeId)
    {
        return await _roleManager.FindByNameAsync(roleName)
            is ApplicationRole existingRole
            && existingRole.Id != excludeId;
    }

    public async Task<RoleResponse> GetByIdAsync(string id)
    {
        return await _dbContext.Roles.SingleOrDefaultAsync(x => x.Id == id) is { } role
            ? role.Adapt<RoleResponse>()
            : throw new NotFoundException("Role not found");
    }

    public async Task<RoleResponse> GetByIdWithPermissionsAsync(string roleId, CancellationToken cancellationToken)
    {
        var role = await GetByIdAsync(roleId);

        role.Permissions = await _dbContext.RoleClaims
            .Where(x => x.RoleId == roleId && x.ClaimType == ScreenDraftsClaims.Permission)
            .Select(x => x.ClaimValue!)
            .ToListAsync(cancellationToken);

        return role;
    }

    public async Task<RoleResponse> GetByNameAsync(string roleName, CancellationToken cancellationToken)
    {
        var role = await _dbContext.Roles.SingleOrDefaultAsync(x => x.Name == roleName, cancellationToken);
        return role!.Adapt<RoleResponse>();
    }

    public async Task<List<RoleResponse>> GetListAsync(CancellationToken cancellationToken)
    {
        // TODO: Add permissions to response
        return (await _roleManager.Roles.ToListAsync(cancellationToken)).Adapt<List<RoleResponse>>();
    }

    public async Task<string> UpdatePermissionsAsync(UpdateRolePermissionsRequest request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId);
        _ = role ?? throw new NotFoundException("Role Not Found");
        if (role.Name == ScreenDraftsRoles.Admin)
        {
            throw new ConflictException("Not allowed to modify Permissions for this Role.");
        }

        var currentClaims = await _roleManager.GetClaimsAsync(role);

        // Remove permissions that were previously selected
        foreach (var claim in currentClaims.Where(c => !request.Permissions.Exists(p => p == c.Value)))
        {
            var removeResult = await _roleManager.RemoveClaimAsync(role, claim);
            if (!removeResult.Succeeded)
            {
                throw new InternalServerException("Update permissions failed.", removeResult.GetErrors());
            }
        }

        // Add all permissions that were not previously selected
        foreach (string permission in request.Permissions.Where(c => !currentClaims.Any(p => p.Value == c)))
        {
            if (!string.IsNullOrEmpty(permission))
            {
                _dbContext.RoleClaims.Add(new ApplicationRoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = ScreenDraftsClaims.Permission,
                    ClaimValue = permission,
                    CreatedBy = _currentUser.GetUserId().ToString(),
                });
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        await _events.PublishAsync(new ApplicationRoleUpdatedEvent(DefaultIdType.NewGuid(), role.Id, role.Name!, true));

        return "Permissions Updated.";
    }
}
