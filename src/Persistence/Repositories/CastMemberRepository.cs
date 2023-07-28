namespace ScreenDrafts.Api.Persistence.Repositories;
public sealed class CastMemberRepository : ICastMemberRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CastMemberRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(CastMember castMember)
    {
        _dbContext.CastMember.Add(castMember);
    }

    public async Task<List<CastMember>> GetAllCastMembers(CancellationToken cancellationToken = default)
    {
        var castMembers = await _dbContext.CastMember.ToListAsync(cancellationToken);
        return castMembers;
    }

    public async Task<CastMember> GetByCastMemberIdAsync(DefaultIdType id, CancellationToken cancellationToken = default)
    {
        var castMembers = await _dbContext.CastMember.ToListAsync(cancellationToken);
        return castMembers.SingleOrDefault(x => x.Id!.Value == id);
    }

    public async Task<CastMember> GetByImdbIdAsync(string imdbId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CastMember.FirstOrDefaultAsync(x => x.ImdbId == imdbId, cancellationToken);
    }

    public async Task<CastMember> GetByName(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CastMember.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }

    public void UpdateCastMember(CastMember castMember)
    {
        throw new NotImplementedException();
    }
}
