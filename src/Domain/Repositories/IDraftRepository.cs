namespace ScreenDrafts.Api.Domain.Repositories;
public interface IDraftRepository
{
    void Add(Draft draft);
    void UpdateDraft(Draft draft);
    Task AddDrafterAsync(Draft draft, Drafter drafter, CancellationToken cancellationToken = default);
    Task AddHostAsync(Draft draft, Host host, CancellationToken cancellationToken = default);
    Task<List<Draft>> GetAllDrafts(CancellationToken cancellationToken = default);
    Task<Draft> GetByIdAsync(DefaultIdType id, CancellationToken cancellationToken = default);
    Task<Pick> GetPickByIdAsync(DefaultIdType id, CancellationToken cancellationToken = default);
}
