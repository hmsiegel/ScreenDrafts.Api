namespace ScreenDrafts.Api.Application.Common.Auditing;

public sealed class AuditLogRequest : IRequest<List<AuditResponse>>
{
}

internal sealed class AuditLogRequestHandler : IRequestHandler<AuditLogRequest, List<AuditResponse>>
{
    private readonly IAuditService _auditService;
    private readonly ICurrentUserService _currentUserService;

    public AuditLogRequestHandler(
        IAuditService auditService,
        ICurrentUserService currentUserService)
    {
        _auditService = auditService;
        _currentUserService = currentUserService;
    }

    public async Task<List<AuditResponse>> Handle(AuditLogRequest request, CancellationToken cancellationToken)
    {
        return await _auditService.GetUserTrailsAsync(_currentUserService.GetUserId());
    }
}
