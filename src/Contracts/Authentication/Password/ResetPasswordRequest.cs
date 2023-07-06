namespace ScreenDrafts.Api.Contracts.Authentication.Password;
public sealed record ResetPasswordRequest(string Email, string Password, string Token);
