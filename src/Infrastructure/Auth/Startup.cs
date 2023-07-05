namespace ScreenDrafts.Api.Infrastructure.Auth;
internal static class Startup
{
    internal static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddIdentity();

        return services;
    }
}
