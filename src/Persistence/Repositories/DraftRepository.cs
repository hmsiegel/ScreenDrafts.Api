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

    public async Task AddDrafterAsync(Draft draft, Drafter drafter)
    {
        var updateDraft = await _context.Drafts
            .Where(d => d.Id == draft.Id)
            .Include(d => d.DrafterIds)
            .FirstOrDefaultAsync();

        updateDraft!.AddDrafter(drafter.Id!);
    }

    public async Task<Draft> GetByIdAsync(DefaultIdType id, CancellationToken cancellationToken = default)
    {
        var drafts = await GetAllDrafts(cancellationToken);
        return drafts.Find(x => x.Id!.Value == id);
    }

    public async Task<List<Draft>> GetAllDrafts(CancellationToken cancellationToken = default)
    {
        return await _context.Drafts
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task AddHostAsync(Draft draft, Host host)
    {
        var updateDraft =await _context.Drafts
            .Where(d => d.Id == draft.Id)
            .Include(d => d.HostIds)
            .FirstOrDefaultAsync();

        updateDraft!.AddHost(host.Id!);
    }

    public void UpdateDraft(Draft draft)
    {
        _context.Drafts.Update(draft);
    }

    public void AddSelectedMovie(Draft draft, SelectedMovie selectedMovie)
    {
        var updateDraft = _context.Drafts
            .Where(d => d.Id == draft.Id)
            .Include(d => d.SelectedMovies)
            .FirstOrDefault();

        updateDraft!.AddSelectedMovie(selectedMovie);
    }

    public List<SelectedMovie> GetSelectedMoviesForDraft(DefaultIdType draftId)
    {
        var draft = _context.Drafts
            .Where(d => d.Id!.Value == draftId)
            .Include(d => d.SelectedMovies)
            .FirstOrDefault();

        var selectedMovies = draft!.SelectedMovies!.ToList();

        return selectedMovies;
    }
}
