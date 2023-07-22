namespace ScreenDrafts.Api.Domain.Repositories;
public interface IDrafterRepository
{
    void Add(Drafter drafter);
    void UpdateDrafter(Drafter drafter);
    Task<Drafter> GetByDrafterIdAsync(DefaultIdType id, CancellationToken cancellationToken = default);
    Task<Drafter> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    Task<List<Drafter>> GetAllDrafters(CancellationToken cancellationToken = default);
}
