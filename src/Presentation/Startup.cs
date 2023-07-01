using Microsoft.Extensions.Configuration;

using ScreenDrafts.Api.Presentation.OpenApi;

namespace ScreenDrafts.Api.Presentation;
public static class Startup
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        services.AddOpenApiDocumentation(config);
        services.AddRouting(options => options.LowercaseUrls = true);

        return services;
    }

    public static IApplicationBuilder UsePresentation(this IApplicationBuilder app)
    {
        app.UseOpenApiDocumentation();
        app.UseHttpsRedirection();

        return app;
    }

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapControllers();

        return endpoints;
    }
}
