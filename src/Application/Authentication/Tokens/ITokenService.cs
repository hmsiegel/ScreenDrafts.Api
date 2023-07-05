namespace ScreenDrafts.Api.Application.Authentication.Tokens;
public interface ITokenService
{
    Task<TokenResponse> GetTokenAsync(TokenRequest request, string ipAddress, CancellationToken cancellationToken);
    Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress);
}
