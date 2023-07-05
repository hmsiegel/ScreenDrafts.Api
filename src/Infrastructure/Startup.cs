namespace ScreenDrafts.Api.Infrastructure;
public static class Startup
{
    public static readonly Assembly AssemblyReference = typeof(Startup).Assembly;
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddAuth();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(ValidationPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        });
        services.AddScoped<IUserService, UserService>();
        services.RegisterServices();

        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services
            .Scan(
            scan => scan
               .FromAssemblies(
                   AssemblyReference,
                   Persistence.Startup.AssemblyReference)
               .AddClasses(false)
               .UsingRegistrationStrategy(RegistrationStrategy.Skip)
               .AsMatchingInterface()
               .WithScopedLifetime());

        return services;
    }
}
