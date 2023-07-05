using Microsoft.AspNetCore.Http;

namespace ScreenDrafts.Api.Infrastructure.FileStorage;
internal static class Startup
{
    internal static IApplicationBuilder UseFileStorage(this IApplicationBuilder app)
    {
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Files")),
            RequestPath = new PathString("/Files"),
        });
        return app;
    }
}
