namespace ScreenDrafts.Api.Infrastructure.Auth;
public sealed class SecuritySettings
{
    public string? Provider { get; set; }
    public bool RequireConfirmedAccount { get; set; }
}
