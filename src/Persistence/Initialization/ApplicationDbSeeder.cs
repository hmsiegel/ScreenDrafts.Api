namespace ScreenDrafts.Api.Persistence.Initialization;
internal sealed class ApplicationDbSeeder
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly CustomSeederRunner _seederRunner;
    private readonly ILogger<ApplicationDbSeeder> _logger;
    private readonly AdminSettings _adminSettings;

    public ApplicationDbSeeder(
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager,
        CustomSeederRunner seederRunner,
        ILogger<ApplicationDbSeeder> logger,
        IOptions<AdminSettings> adminSettings)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _seederRunner = seederRunner;
        _logger = logger;
        _adminSettings = adminSettings.Value;
    }

    public async Task SeedDatabaseAsync(ApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        await SeedRolesAsync(dbContext);
        await SeedAdminUserAsync();
        await _seederRunner.RunSeedersAsync(cancellationToken);
    }

    private async Task SeedAdminUserAsync()
    {
        if (await _userManager.Users.FirstOrDefaultAsync(u => u.Email == _adminSettings.Email) is not ApplicationUser adminUser)
        {
            string adminUserName = $"{_adminSettings.FirstName} {_adminSettings.LastName}".ToLowerInvariant();
            adminUser = new ApplicationUser
            {
                Email = _adminSettings.Email,
                UserName = adminUserName,
                FirstName = _adminSettings.FirstName,
                LastName = _adminSettings.LastName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                NormalizedEmail = _adminSettings.Email.ToUpperInvariant(),
                NormalizedUserName = adminUserName.ToUpperInvariant(),
                IsActive = true,
            };

            _logger.LogInformation("Creating admin user '{UserName}'", adminUser.UserName);
            adminUser.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(adminUser, _adminSettings.Password);
            await _userManager.CreateAsync(adminUser);
        }

        // Assign all permissions to the admin user
        if (!await _userManager.IsInRoleAsync(adminUser, ScreenDraftsRoles.Admin))
        {
            _logger.LogInformation("Assigning admin role to user '{UserName}'", adminUser.UserName);
            await _userManager.AddToRoleAsync(adminUser, ScreenDraftsRoles.Admin);
        }
    }

    private async Task SeedRolesAsync(ApplicationDbContext dbContext)
    {
        foreach (string roleName in ScreenDraftsRoles.DefaultRoles)
        {
            if (await _roleManager.Roles.SingleOrDefaultAsync(r => r.Name == roleName)
                is not ApplicationRole role)
            {
                // Create the role
                _logger.LogInformation("Creating role '{role}'", roleName);
                role = new ApplicationRole(roleName);
                await _roleManager.CreateAsync(role);
            }

            // Assign all permissions to the role
            switch (roleName)
            {
                case ScreenDraftsRoles.Admin:
                    await AssignPermissionsToRoleAsync(dbContext, ScreenDraftsPermissions.Admin, role);
                    break;
                case ScreenDraftsRoles.Drafter:
                    await AssignPermissionsToRoleAsync(dbContext, ScreenDraftsPermissions.Drafter, role);
                    break;
                case ScreenDraftsRoles.Commissioner:
                    await AssignPermissionsToRoleAsync(dbContext, ScreenDraftsPermissions.Host, role);
                    break;
                case ScreenDraftsRoles.Basic:
                    await AssignPermissionsToRoleAsync(dbContext, ScreenDraftsPermissions.Basic, role);
                    break;
            }
        }
    }

    private async Task AssignPermissionsToRoleAsync(ApplicationDbContext dbContext, IReadOnlyList<ScreenDraftsPermission> permissions, ApplicationRole role)
    {
        var currentClaims = await _roleManager.GetClaimsAsync(role);
        foreach (var permission in permissions)
        {
            if (!currentClaims.Any(c => c.Type == ScreenDraftsClaims.Permission && c.Value == permission.Name))
            {
                _logger.LogInformation("Seeding permission '{PermissionName}' to role '{RoleName}'", permission.Name, role.Name);
                dbContext.RoleClaims.Add(new ApplicationRoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = ScreenDraftsClaims.Permission,
                    ClaimValue = permission.Name,
                    CreatedBy = "ApplicationDbSeeder",
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
