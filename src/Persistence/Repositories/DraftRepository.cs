namespace ScreenDrafts.Api.Persistence.Repositories;
internal sealed class DraftRepository : IDraftRepository
{
    private readonly ApplicationDbContext _context;

    public DraftRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Draft draft)
    {
        _context.Drafts.Add(draft);
    }

    public Task<Draft> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
