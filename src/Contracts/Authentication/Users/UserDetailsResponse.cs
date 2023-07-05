namespace ScreenDrafts.Api.Contracts.Authentication.Users;
public sealed record UserDetailsResponse(
    DefaultIdType Id,
    string UserName,
    string Email,
    string FirstName,
    string LastName,
    string PhoneNumber,
    bool IsActive,
    bool EmailConfirmed,
    string ImageUrl);
