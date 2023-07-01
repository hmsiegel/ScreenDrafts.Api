using Microsoft.AspNetCore.Routing;

namespace ScreenDrafts.Api.Presentation;
public static class Startup
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static IApplicationBuilder UsePresentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        return app;
    }

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapControllers();

        return endpoints;
    }
}
