namespace ScreenDrafts.Api.Contracts.Authentication;
public sealed record UpdateUserRequest(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber);
