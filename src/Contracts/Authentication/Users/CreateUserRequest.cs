namespace ScreenDrafts.Api.Contracts.Authentication.Users;
public sealed record CreateUserRequest(
    string FirstName,
    string LastName,
    string UserName);
