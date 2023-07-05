namespace ScreenDrafts.Api.Contracts.Authentication.Users;
public sealed record UpdateUserRequest(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    FileUploadRequest? Image,
    bool DeleteCurrentImage);
