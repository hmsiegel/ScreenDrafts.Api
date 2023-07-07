namespace ScreenDrafts.Api.Infrastructure;
public static class Startup
{
    public static readonly Assembly AssemblyReference = typeof(Startup).Assembly;

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var applicationAssembly = typeof(Application.Startup).GetTypeInfo().Assembly;

        services
            .AddAuth(config)
            .AddBehaviors(applicationAssembly)
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(ValidationPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            })
            .AddServices();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder)
    {
        builder
            .UseCurrentUser()
            .UseFileStorage()
            .UseAuthentication()
            .UseAuthorization();

        return builder;
    }
}
