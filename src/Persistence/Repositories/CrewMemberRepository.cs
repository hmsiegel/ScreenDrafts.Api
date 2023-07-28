namespace ScreenDrafts.Api.Persistence.Repositories;
public sealed class CrewMemberRepository : ICrewMemberRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CrewMemberRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(CrewMember crewMember)
    {
        _dbContext.CrewMember.Add(crewMember);
    }

    public async Task<List<CrewMember>> GetAllCrewMembers(CancellationToken cancellationToken = default)
    {
        return await _dbContext.CrewMember.ToListAsync(cancellationToken);
    }

    public async Task<CrewMember> GetByCrewMemberIdAsync(DefaultIdType id, CancellationToken cancellationToken = default)
    {
        var crewMembers = await _dbContext.CrewMember.ToListAsync(cancellationToken);
        return crewMembers.SingleOrDefault(x => x.Id!.Value == id);
    }

    public async Task<CrewMember> GetByImdbIdAsync(string imdbId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CrewMember.FirstOrDefaultAsync(x => x.ImdbId == imdbId, cancellationToken);
    }

    public async Task<CrewMember> GetByName(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.CrewMember.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }

    public void UpdateCrewMember(CrewMember crewMember)
    {
        throw new NotImplementedException();
    }
}
