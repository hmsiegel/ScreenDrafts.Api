namespace ScreenDrafts.Api.Persistence.Identity.Users;
internal sealed partial class UserService
{
    public async Task ChangePasswordAsync(ChangePasswordRequest request, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new NotFoundException("User not found.");

        var result = await _userManager.ChangePasswordAsync(user, request.Password, request.NewPassword);

        if (!result.Succeeded)
        {
            throw new InternalServerException("Change password failed.");
        }
    }

    public async Task<string> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
    {
        const string route = "account/reset-password";

        var user = await _userManager.FindByEmailAsync(request.Email.Normalize());
        if (user is null || !await _userManager.IsEmailConfirmedAsync(user))
        {
            throw new InternalServerException("An error has occurred.");
        }

        string code = await _userManager.GeneratePasswordResetTokenAsync(user);

        var endpointUri = new Uri(string.Concat($"{origin}/", route));
        string passwordResetUri = QueryHelpers.AddQueryString(endpointUri.ToString(), "token", code);

        var mailRequest = new MailRequest(
            new List<string> { request.Email },
            "Reset Password",
            $"Your password reset token is: {code}. You can reset your password using the {passwordResetUri} Endpoint.");
        _jobService.Enqueue(() => _mailService.SendAsync(mailRequest, CancellationToken.None));
        return "Password reset email has been sent to your email address.";
    }

    public async Task<string> ResetPasswordAsync(ResetPasswordRequest request, string origin)
    {
        var user = await _userManager.FindByEmailAsync(request.Email.Normalize())
            ?? throw new InternalServerException("An error has occurred.");

        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

        return result.Succeeded
            ? "Password reset successful."
            : "Password reset failed.";
    }
}
