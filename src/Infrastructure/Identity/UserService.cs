﻿namespace ScreenDrafts.Api.Infrastructure.Identity;
public sealed class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IFileStorageService _fileStorage;
    private readonly IEventPublisher _events;

    public UserService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        SignInManager<ApplicationUser> signInManager,
        IFileStorageService fileStorage,
        IEventPublisher events)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _fileStorage = fileStorage;
        _events = events;
    }

    public async Task<string> CreateAsync(RegisterRequest request, string origin)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber,
            IsActive = true,
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new InternalServerException("Validation errors occurred.", result.GetErrors());
        }

        await _userManager.AddToRoleAsync(user, ScreenDraftsRoles.Basic);

        var messages = new List<string> { string.Format("User {0} registered.", user.UserName) };

        await _events.PublishAsync(new ApplicationUserCreatedEvent(Guid.NewGuid(), user.Id));

        return string.Join(Environment.NewLine, messages);
    }

    public async Task<bool> ExistsWithEmailAsync(string email, string? exceptId = null)
    {
        return await _userManager.FindByEmailAsync(email.Normalize()) is ApplicationUser user && user.Id.ToString() != exceptId;
    }

    public async Task<bool> ExistsWithNameAsync(string name)
    {
        return await _userManager.FindByNameAsync(name) is not null;
    }

    public async Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber, string? exceptId = null)
    {
        return await _userManager.Users
            .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber) is ApplicationUser user && user.Id.ToString() != exceptId;
    }

    public async Task<UserDetailsResponse> GetAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .AsNoTracking()
            .Where(u => u.Id.ToString() == userId)
            .FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException("User not found.");

        return user.Adapt<UserDetailsResponse>();
    }

    public async Task<List<UserDetailsResponse>> GetListAsync(CancellationToken cancellationToken)
    {
        return (await _userManager.Users
            .AsNoTracking()
            .ToListAsync(cancellationToken))
        .Adapt<List<UserDetailsResponse>>();
    }

    public async Task<string> GetOrCreateFromPrincipalAsync(ClaimsPrincipal principal)
    {
        string? objectId = principal.GetObjectId();
        if (string.IsNullOrWhiteSpace(objectId))
        {
            throw new InternalServerException("Invalid objectId");
        }

        var user = await _userManager.Users.Where(u => u.ObjectId == objectId).FirstOrDefaultAsync()
            ?? await CreateOrUpdateFromPrincipalAsync(principal);

        if (principal.FindFirstValue(ClaimTypes.Role) is string role && await _roleManager.RoleExistsAsync(role) && !await _userManager.IsInRoleAsync(user, role))
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        return user.Id.ToString();
    }

    public async Task UpdateAsync(UpdateUserRequest request, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        _ = user ?? throw new NotFoundException("User not found.");

        string currentImage = user.ImageUrl ?? string.Empty;

        if (request.Image is not null || request.DeleteCurrentImage)
        {
            user.ImageUrl = await _fileStorage.UploadAsync<ApplicationUser>(request.Image, FileType.Image);
            if (request.DeleteCurrentImage && !string.IsNullOrEmpty(currentImage))
            {
                string root = Directory.GetCurrentDirectory();
                _fileStorage.Remove(Path.Combine(root, currentImage));
            }
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        string? phoneNumber = await _userManager.GetPhoneNumberAsync(user);
        if (request.PhoneNumber != phoneNumber)
        {
            await _userManager.SetPhoneNumberAsync(user, request.PhoneNumber);
        }

        var result = await _userManager.UpdateAsync(user);

        await _signInManager.RefreshSignInAsync(user);

        await _events.PublishAsync(new ApplicationUserUpdatedEvent(Guid.NewGuid(), user.Id));

        if (!result.Succeeded)
        {
            throw new InternalServerException("Update profile failed.", result.GetErrors());
        }
    }

    private async Task<ApplicationUser> CreateOrUpdateFromPrincipalAsync(ClaimsPrincipal principal)
    {
        string? email = principal.FindFirstValue(ClaimTypes.Upn);
        string? username = principal.GetDisplayName();
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username))
        {
            throw new InternalServerException("Username or Email not valid.");
        }

        var user = await _userManager.FindByNameAsync(username);
        if (user is not null && !string.IsNullOrWhiteSpace(user.ObjectId))
        {
            throw new InternalServerException("Username {0} is already taken.");
        }

        if (user is null)
        {
            user = await _userManager.FindByEmailAsync(email);
            if (user is not null && !string.IsNullOrWhiteSpace(user.ObjectId))
            {
                throw new InternalServerException("Email {0} is already taken.");
            }
        }

        IdentityResult? result;
        if (user is not null)
        {
            user.ObjectId = principal.GetObjectId();
            result = await _userManager.UpdateAsync(user);

            await _events.PublishAsync(new ApplicationUserUpdatedEvent(Guid.NewGuid(), user.Id));
        }
        else
        {
            user = new ApplicationUser
            {
                ObjectId = principal.GetObjectId(),
                FirstName = principal.FindFirstValue(ClaimTypes.GivenName),
                LastName = principal.FindFirstValue(ClaimTypes.Surname),
                Email = email,
                NormalizedEmail = email.ToUpperInvariant(),
                UserName = username,
                NormalizedUserName = username.ToUpperInvariant(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true,
            };
            result = await _userManager.CreateAsync(user);

            await _events.PublishAsync(new ApplicationUserCreatedEvent(Guid.NewGuid(), user.Id));
        }

        if (!result.Succeeded)
        {
            throw new InternalServerException("Validation Errors Occurred.");
        }

        return user;
    }
}
