namespace ScreenDrafts.Api.Presentation.OpenApi;
internal static class Startup
{
    internal static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services, IConfiguration config)
    {
        var settings = config.GetSection(nameof(OpenApiSettings)).Get<OpenApiSettings>();
        if (settings is null)
        {
            return services;
        }

        if (settings.Enable)
        {
            services.AddEndpointsApiExplorer();
            services.AddOpenApiDocument((document, sp) =>
            {
                document.PostProcess = doc =>
                {
                    doc.Info.Title = settings.Title;
                    doc.Info.Description = settings.Description;
                    doc.Info.Version = settings.Version;
                    doc.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = settings.ContactName,
                        Email = settings.ContactEmail,
                        Url = settings.ContactUrl,
                    };
                    doc.Info.License = new()
                    {
                        Name = settings.LicenseName,
                        Url = settings.LicenseUrl,
                    };
                };
            });
        }

        return services;
    }

    internal static IApplicationBuilder UseOpenApiDocumentation(this IApplicationBuilder app)
    {
        app.UseOpenApi();
        app.UseSwaggerUi3();

        return app;
    }
}
