namespace ScreenDrafts.Api.Application.Common.Auditing;
public interface IAuditService : ITransientService
{
    Task<List<AuditResponse>> GetUserTrailsAsync(DefaultIdType userId);
}
