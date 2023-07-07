namespace ScreenDrafts.Api.Persistence.Identity.Users;
internal sealed partial class UserService
{
    public async Task<string> AssignRolesAsync(string userId, UserRolesRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _userManager.Users.Where(u => u.Id.ToString() == userId).FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException("User not found.");

        // Check if the user is an admin for which the admin role is getting disabled
        if (await _userManager.IsInRoleAsync(user, ScreenDraftsRoles.Admin)
            && request.UserRoles.Exists(a => !a.IsEnabled && a.RoleName == ScreenDraftsRoles.Admin))
        {
            // Get the count of the users in the admin role
            int adminCount = (await _userManager.GetUsersInRoleAsync(ScreenDraftsRoles.Admin)).Count;

            if (adminCount <= 1)
            {
                throw new ConflictException("Cannot remove the only administrator.");
            }
        }

        foreach (var userRole in from userRole in request.UserRoles// Check if role exists
                                 where _roleManager.FindByNameAsync(userRole.RoleName!) is not null
                                 select userRole)
        {
            if (userRole.IsEnabled)
            {
                if (!await _userManager.IsInRoleAsync(user, userRole.RoleName!))
                {
                    await _userManager.AddToRoleAsync(user, userRole.RoleName!);
                }
            }
            else
            {
                await _userManager.RemoveFromRoleAsync(user, userRole.RoleName!);
            }
        }

        await _events.PublishAsync(new ApplicationUserUpdatedEvent(DefaultIdType.NewGuid(), DefaultIdType.Parse(userId), true));

        return "User roles updated.";
    }

    public async Task<List<UserRoleResponse>> GetRolesAsync(string userId, CancellationToken cancellationToken)
    {
        var userRoles =  new List<UserRoleResponse>();

        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new NotFoundException("User not found.");
        var roles = await _roleManager.Roles.AsNoTracking().ToListAsync(cancellationToken)
            ?? throw new NotFoundException("Roles not found.");

        foreach (var role in roles)
        {
            userRoles.Add(new UserRoleResponse(
                role.Id.ToString(),
                role.Name!,
                role.Description!,
                await _userManager.IsInRoleAsync(user, role.Name!)));
        }

        return userRoles;
    }
}
