namespace ScreenDrafts.Api.Contracts.Authentication.Password;
public sealed record ChangePasswordRequest(
    string Password,
    string NewPassword,
    string ConfirmPassword);
