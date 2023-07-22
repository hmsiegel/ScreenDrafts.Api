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

    public async Task<Drafter> GetByDrafterIdAsync(DefaultIdType id, CancellationToken cancellationToken = default)
    {
        var drafters = await _context.Drafters.ToListAsync(cancellationToken: cancellationToken);

        return drafters.SingleOrDefault(d => d.Id!.Value == id);
    }

    public async Task<Drafter> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var drafters = await _context.Drafters
            .Include(d => d.User)
            .ToListAsync(cancellationToken);

        return drafters.SingleOrDefault(d => d.User!.Id == userId);
    }

    public void UpdateDrafter(Drafter drafter)
    {
        _context.Drafters.Update(drafter);
    }
}
