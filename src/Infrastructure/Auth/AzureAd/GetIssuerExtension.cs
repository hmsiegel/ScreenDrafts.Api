namespace ScreenDrafts.Api.Infrastructure.Auth.AzureAd;
public static class GetIssuerExtension
{
    public static string? GetIssuer(this ClaimsPrincipal principal)
    {
        if (principal.FindFirstValue(OpenIdConnectClaimTypes.Issuer) is string issuer)
        {
            return issuer;
        }

        return principal.FindFirst(AzureAdClaimTypes.ObjectId)?.Issuer;
    }
}
