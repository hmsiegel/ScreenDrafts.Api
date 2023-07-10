using ScreenDrafts.Api.Contracts.Mailing;

namespace ScreenDrafts.Api.Persistence.Identity.Users;
internal sealed partial class UserService
{
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

        return user.Id;
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

        var messages = new List<string> { $"User {user.UserName} registered." };

        if (_securitySettings.RequireConfirmedAccount && !string.IsNullOrEmpty(user.Email))
        {
            string emailVerificationUri = await GetEmailVerificationUriAsync(user, origin);
            RegisterUserEmailModel emailModel = new(
                user.UserName,
                user.Email,
                emailVerificationUri);

            var mailRequest = new MailRequest(
                new List<string> { user.Email },
                "Confirm your registration",
                _templateService.GenericEmailTemplate("email-confirmation", emailModel));
            _jobService.Enqueue(() => _mailService.SendAsync(mailRequest, CancellationToken.None));
            messages.Add("Please check your email to verify your account.");
        }

        await _events.PublishAsync(new ApplicationUserCreatedEvent(DefaultIdType.NewGuid(), DefaultIdType.Parse(user.Id)));

        return string.Join(Environment.NewLine, messages);
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

        await _events.PublishAsync(new ApplicationUserUpdatedEvent(DefaultIdType.NewGuid(), DefaultIdType.Parse(user.Id)));

        if (!result.Succeeded)
        {
            throw new InternalServerException("Update profile failed.", result.GetErrors());
        }
    }

    public async Task ToggleStatusAsync(ToggleUserStatusRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.Where(u => u.Id == request.UserId).FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException("User not found.");

        bool isAdmin = await _userManager.IsInRoleAsync(user, ScreenDraftsRoles.Admin);
        if (isAdmin)
        {
            throw new ConflictException("Administrator's status cannot be changed.");
        }

        user .IsActive = request.ActivateUser;
        await _userManager.UpdateAsync(user);
        await _events.PublishAsync(new ApplicationUserUpdatedEvent(DefaultIdType.NewGuid(), DefaultIdType.Parse(user.Id)));
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

            await _events.PublishAsync(new ApplicationUserUpdatedEvent(DefaultIdType.NewGuid(), DefaultIdType.Parse(user.Id)));
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

            await _events.PublishAsync(new ApplicationUserCreatedEvent(DefaultIdType.NewGuid(), DefaultIdType.Parse(user.Id)));
        }

        if (!result.Succeeded)
        {
            throw new InternalServerException("Validation Errors Occurred.");
        }

        return user;
    }
}
