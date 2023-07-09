namespace ScreenDrafts.Api.Infrastructure.Mailing;
internal static class Startup
{
    internal static IServiceCollection AddMailing(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));
        return services;
    }
}
