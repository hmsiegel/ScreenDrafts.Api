namespace ScreenDrafts.Api.Infrastructure.BackgroundJobs;
public class ScreenDraftsJobFilter : IClientFilter
{
    private static readonly ILog _logger = LogProvider.GetCurrentClassLogger();

    private readonly IServiceProvider _services;

    public ScreenDraftsJobFilter(IServiceProvider services) => _services = services;

    public void OnCreating(CreatingContext filterContext)
    {
        ArgumentNullException.ThrowIfNull(filterContext);

        _logger.InfoFormat("Set TenantId and UserId parameters to job {0}.{1}...", filterContext.Job.Method.ReflectedType?.FullName, filterContext.Job.Method.Name);

        using var scope = _services.CreateScope();

        var httpContext = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
        _ = httpContext ?? throw new InvalidOperationException("Can't create a TenantJob without HttpContext.");

        string? userId = httpContext.User.GetUserId();
        filterContext.SetJobParameter(QueryStringKeys.UserId, userId);
    }

    public void OnCreated(CreatedContext filterContext) =>
        _logger.InfoFormat(
            "Job created with parameters {0}",
            filterContext.Parameters.Select(x => x.Key + "=" + x.Value).Aggregate((s1, s2) => s1 + ";" + s2));
}
