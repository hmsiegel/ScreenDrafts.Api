namespace ScreenDrafts.Api.Infrastructure.Middleware;
public class RequestLoggingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        LogContext.PushProperty("RequestTimeUTC", DateTime.UtcNow);
        string requestBody = string.Empty;
        if (context.Request.Path.ToString().Contains("tokens", StringComparison.OrdinalIgnoreCase))
        {
            requestBody = "[Redacted] Contains Sensitive Information.";
        }
        else if (context.Request.Path.ToString().Contains("jobs", StringComparison.OrdinalIgnoreCase))
        {
            await next(context);
        }
        else
        {
            var request = context.Request;

            if (!string.IsNullOrEmpty(request.ContentType)
                && request.ContentType.StartsWith("application/json"))
            {
                request.EnableBuffering();
                using var reader = new StreamReader(request.Body, Encoding.UTF8, true, 4096, true);
                requestBody = await reader.ReadToEndAsync();

                // rewind for next middleware.
                request.Body.Position = 0;
            }
        }

        LogContext.PushProperty("RequestBody", requestBody);
        Log.ForContext("RequestHeaders", context.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
           .ForContext("RequestBody", requestBody)
           .Information("HTTP {RequestMethod} Request sent to {RequestPath}", context.Request.Method, context.Request.Path);
        await next(context);
    }
}
