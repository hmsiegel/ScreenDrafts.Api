namespace ScreenDrafts.Api.Contracts.Authentication.Users;
public sealed record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string Password,
    string ConfirmPassword,
    string PhoneNumber);
