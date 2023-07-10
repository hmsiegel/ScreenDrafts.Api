namespace ScreenDrafts.Api.Persistence.Auditing;
public sealed class AuditService : IAuditService
{
    private readonly ApplicationDbContext _context;

    public AuditService(ApplicationDbContext context) => _context = context;

    public async Task<List<AuditResponse>> GetUserTrailsAsync(DefaultIdType userId)
    {
        var trails = await _context.AuditTrails
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.CreatedAt)
            .Take(200)
            .ToListAsync();

        return trails.Adapt<List<AuditResponse>>();
    }
}
