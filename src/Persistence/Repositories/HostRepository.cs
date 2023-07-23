namespace ScreenDrafts.Api.Persistence.Repositories;
internal sealed class HostRepository : IHostRepository
{
    private readonly ApplicationDbContext _context;

    public HostRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Host host)
    {
        _context.Hosts.Add(host);
    }

    public async Task<List<Host>> GetAllHosts(CancellationToken cancellationToken = default)
    {
        return await _context.Hosts
            .Include(d => d.User)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<Host> GetByHostIdAsync(DefaultIdType id, CancellationToken cancellationToken = default)
    {
        var hosts = await _context.Hosts.ToListAsync(cancellationToken: cancellationToken);

        return hosts.SingleOrDefault(d => d.Id!.Value == id);
    }

    public async Task<Host> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var hosts = await _context.Hosts
            .Include(d => d.User)
            .ToListAsync(cancellationToken);

        return hosts.SingleOrDefault(d => d.User!.Id == userId);
    }

    public void UpdateHost(Host host)
    {
        _context.Hosts.Update(host);
    }
}
