namespace ScreenDrafts.Api.Infrastructure.Middleware;
public class ResponseLoggingMiddleware : IMiddleware
{
    private readonly ICurrentUserService _currentUser;

    public ResponseLoggingMiddleware(ICurrentUserService currentUser) => _currentUser = currentUser;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next(context);
        var originalBody = context.Response.Body;
        using var newBody = new MemoryStream();
        context.Response.Body = newBody;
        string responseBody;
        if (context.Request.Path.ToString().Contains("tokens", StringComparison.OrdinalIgnoreCase))
        {
            responseBody = "[Redacted] Contains Sensitive Information.";
        }
        else if (context.Request.Path.ToString().Contains("jobs", StringComparison.OrdinalIgnoreCase))
        {
            newBody.Seek(0, SeekOrigin.Begin);
            await newBody.CopyToAsync(originalBody);
            return;
        }
        else
        {
            newBody.Seek(0, SeekOrigin.Begin);
            responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
        }

        string email = _currentUser.GetUserEmail() is string userEmail ? userEmail : "Anonymous";
        var userId = _currentUser.GetUserId();
        if (userId != DefaultIdType.Empty)
        {
            LogContext.PushProperty("UserId", userId);
        }

        LogContext.PushProperty("UserEmail", email);
        LogContext.PushProperty("StatusCode", context.Response.StatusCode);
        LogContext.PushProperty("ResponseTimeUTC", DateTime.UtcNow);

        Log.ForContext(
            "ResponseHeaders",
            context.Response.Headers.ToDictionary(
                h => h.Key,
                h => h.Value.ToString()),
            destructureObjects: true)
           .ForContext(
            "ResponseBody",
            responseBody)
           .Information(
            "HTTP {RequestMethod} Request to {RequestPath} by {RequesterEmail} has Status Code {StatusCode}.",
            context.Request.Method,
            context.Request.Path,
            string.IsNullOrEmpty(email) ? "Anonymous" : email,
            context.Response.StatusCode);
        newBody.Seek(0, SeekOrigin.Begin);
        await newBody.CopyToAsync(originalBody);
    }
}
