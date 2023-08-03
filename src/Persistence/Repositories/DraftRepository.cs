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

    public async Task AddDrafterAsync(Draft draft, Drafter drafter, CancellationToken cancellationToken = default)
    {
        var updateDraft = await _context.Drafts
            .Where(d => d.Id == draft.Id)
            .Include(d => d.DrafterIds)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

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

    public async Task AddHostAsync(Draft draft, Host host, CancellationToken cancellationToken = default)
    {
        var updateDraft = await _context.Drafts
            .Where(d => d.Id == draft.Id)
            .Include(d => d.HostIds)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        updateDraft!.AddHost(host.Id!);
    }

    public void UpdateDraft(Draft draft)
    {
        _context.Drafts.Update(draft);
    }

    public void AddDraftPick(Draft draft, Pick pick)
    {
        var updatedDraft = _context.Drafts
            .Where(d => d.Id == draft.Id)
            .Include(d => d.Picks)
            .FirstOrDefault();

        updatedDraft!.AddPick(pick);
    }

    public Task<Pick> GetPickByIdAsync(DefaultIdType id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<BlessingDecision>> GetBlessingDecisionsAsync(
        Draft draft,
        Pick pick,
        PickDecision pickDecision,
        CancellationToken cancellationToken = default)
    {
        var currentDraft = await _context.Drafts
            .Where(d => d.Id == draft.Id)
            .Include(d => d.Picks)
            .FirstOrDefaultAsync(cancellationToken);

        var currentPick = currentDraft!.Picks!
            .FirstOrDefault(p => p.Id == pick.Id);

        var currentPickDecision = currentPick!.PickDecisions!
            .FirstOrDefault(pd => pd.Id == pickDecision.Id);

        return currentPickDecision!.BlessingDecisions!.ToList();
    }
}
