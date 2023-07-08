using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

using ScreenDrafts.Api.Presentation.OpenApi;

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
            .AddBehaviors(applicationAssembly)
            .AddCaching(config)
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(ValidationPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            })
            .AddOpenApiDocumentation(config)
            .AddRouting(options => options.LowercaseUrls = true)
            .AddServices();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config)
    {
        builder
            .UseAuthentication()
            .UseAuthorization()
            .UseCurrentUser()
            .UseFileStorage()
            .UseSecurityHeaders(config)
            .UseOpenApiDocumentation(config)
            .UseRouting();

        return builder;
    }

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapControllers();

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
