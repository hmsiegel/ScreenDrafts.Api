namespace ScreenDrafts.Api.Host;
internal static class Startup
{
    internal static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services
            .Scan(
            scan => scan
               .FromAssemblies(
                   Infrastructure.Startup.AssemblyReference,
                   Persistence.Startup.AssemblyReference)
               .AddClasses(false)
               .UsingRegistrationStrategy(RegistrationStrategy.Skip)
               .AsMatchingInterface()
               .WithScopedLifetime());

        return services;
    }
}
