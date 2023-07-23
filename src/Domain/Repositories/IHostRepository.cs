namespace ScreenDrafts.Api.Domain.Repositories;
public interface IHostRepository
{
    void Add(Host host);
    void UpdateHost(Host host);
    Task<Host> GetByHostIdAsync(DefaultIdType id, CancellationToken cancellationToken = default);
    Task<Host> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    Task<List<Host>> GetAllHosts(CancellationToken cancellationToken = default);
}
