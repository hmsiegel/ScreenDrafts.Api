namespace ScreenDrafts.Api.Persistence.Identity.Users;
internal sealed partial class UserService
{
    public async Task<string> ConfirmEmailAsync(string userId, string code, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Where(u => u.Id == userId && !u.EmailConfirmed)
            .FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new InternalServerException("An error occurred while confirming E-Mail.");

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ConfirmEmailAsync(user, code);

        return result.Succeeded
            ? $"Account Confirmed for E-Mail {user.Email}. You can now use the /api/tokens endpoint to generate JWT."
            : throw new InternalServerException($"An error occurred while confirming {user.Email}");
    }

    public async Task<string> ConfirmPhoneNumberAsync(string userId, string code)
    {
        var user = await _userManager.FindByIdAsync(userId);

        _ = user ?? throw new InternalServerException("An error occurred while confirming Mobile Phone.");
        if (string.IsNullOrEmpty(user.PhoneNumber))
        {
            throw new InternalServerException("An error occurred while confirming Mobile Phone.");
        }

        var result = await _userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, code);

        if (result.Succeeded)
        {
            return user.PhoneNumberConfirmed
                ? $"Account Confirmed for Phone Number {user.PhoneNumber}. You can now use the /api/tokens endpoint to generate JWT."
                : $"Account Confirmed for Phone Number {user.PhoneNumber}. You should confirm your E-mail before using the /api/tokens endpoint to generate JWT.";
        }
        else
        {
            throw new InternalServerException($"An error occurred while confirming {user.PhoneNumber}");
        }
    }

    public async Task<string> GetEmailVerificationUriAsync(ApplicationUser user, string origin)
    {
        const string route = "api/users/confirm-email";
        string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        var endpointUri = new Uri(string.Concat($"{origin}/", route));
        string verificationUri = QueryHelpers.AddQueryString(endpointUri.ToString(), QueryStringKeys.UserId, user.Id);
        return QueryHelpers.AddQueryString(verificationUri, QueryStringKeys.Code, code);
    }
}
