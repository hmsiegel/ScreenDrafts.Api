namespace ScreenDrafts.Api.Contracts.Authentication.Users;
public sealed record ToggleUserStatusRequest(bool ActivateUser, string UserId);
