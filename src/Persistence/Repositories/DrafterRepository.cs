namespace ScreenDrafts.Api.Persistence.Repositories;
internal sealed class DrafterRepository : IDrafterRepository
{
    private readonly ApplicationDbContext _context;

    public DrafterRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Drafter drafter)
    {
        _context.Drafters.Add(drafter);
    }

    public async Task<List<Drafter>> GetAllDrafters(CancellationToken cancellationToken = default)
    {
        return await _context.Drafters
            .Include(d => d.User)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<Drafter> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Drafters
            .Include(d => d.User)
            .SingleOrDefaultAsync(d => d.Id!.ToString()! == id, cancellationToken: cancellationToken);
    }

    public async Task<Drafter> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _context.Drafters
            .Include(d => d.User)
            .SingleOrDefaultAsync(d => d.UserId == userId, cancellationToken: cancellationToken);
    }
}
