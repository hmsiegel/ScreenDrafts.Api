namespace ScreenDrafts.Api.Domain.Repositories;
public interface IDraftRepository
{
    void Add(Draft draft);
    Task<Draft> GetByIdAsync(string id, CancellationToken cancellationToken = default);
}
