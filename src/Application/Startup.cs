namespace ScreenDrafts.Api.Application;
public static class Startup
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return services
            .AddBehaviors()
            .AddValidatorsFromAssembly(assembly, includeInternalTypes: true)
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
    }
}
