using ScreenDrafts.Api.Application.Authentication.Users;
using ScreenDrafts.Api.Contracts.Authentication.Users;
using ScreenDrafts.Api.Shared.Authorization;

namespace ScreenDrafts.Api.Infrastructure.Identity;
public sealed class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
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

        var messages = new List<string> { "Account registered. Please check your email for verification instructions." };

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

    public Task<string> GetOrCreateFromPrincipalAsync(ClaimsPrincipal principal)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UpdateUserRequest request, string userId)
    {
        throw new NotImplementedException();
    }
}
