namespace ScreenDrafts.Api.Domain.Repositories;
public interface IDraftRepository
{
    void Add(Draft draft);
    Task AddDrafterAsync(Draft draft, Drafter drafter);
    void UpdateDraft(Draft draft);
    Task AddHostAsync(Draft draft, Host host);
    Task<List<Draft>> GetAllDrafts(CancellationToken cancellationToken = default);
    Task<Draft> GetByIdAsync(DefaultIdType id, CancellationToken cancellationToken = default);
    List<SelectedMovie> GetSelectedMoviesForDraft(DefaultIdType draftId);
}
