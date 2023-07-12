namespace ScreenDrafts.Api.Infrastructure;
public static class Startup
{
    public static readonly Assembly AssemblyReference = typeof(Startup).Assembly;

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var applicationAssembly = typeof(Application.Startup).GetTypeInfo().Assembly;

        services
            .AddApiVersioning()
            .AddAuth(config)
            .AddBackgroundJobs(config)
            .AddBehaviors(applicationAssembly)
            .AddCaching(config)
            .AddCorsPolicy(config)
            .AddExceptionMiddleware()
            .AddImdb(config)
            .AddMailing(config)
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            })
            .AddNotifications(config)
            .AddOpenApiDocumentation(config)
            .AddRequestLogging(config)
            .AddRouting(options => options.LowercaseUrls = true)
            .AddServices();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config)
    {
        builder
            .UseCurrentUser()
            .UseExceptionMiddleware()
            .UseFileStorage()
            .UseHangfireDashboard()
            .UseHttpsRedirection()
            .UseRequestLogging(config)
            .UseSecurityHeaders(config)
            .UseOpenApiDocumentation(config)
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization();

        return builder;
    }

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapControllers().RequireAuthorization();
        endpoints.MapNotifications();

        return endpoints;
    }

    private static IServiceCollection AddApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        return services;
    }
}
