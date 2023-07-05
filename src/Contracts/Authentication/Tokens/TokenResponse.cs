namespace ScreenDrafts.Api.Contracts.Authentication.Tokens;
public sealed record TokenResponse(string Token, string RefreshToken, DateTime RefreshTokenExpirationTime);
