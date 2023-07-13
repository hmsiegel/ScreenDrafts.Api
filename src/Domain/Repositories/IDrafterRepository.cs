namespace ScreenDrafts.Api.Domain.Repositories;
public interface IDrafterRepository
{
    void Add(Drafter drafter);
    Task<Drafter> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<Drafter> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
}
