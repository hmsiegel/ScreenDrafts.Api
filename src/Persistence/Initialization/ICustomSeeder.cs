namespace ScreenDrafts.Api.Persistence.Initialization;
public interface ICustomSeeder
{
    Task InitializeAsync(CancellationToken cancellationToken = default);
}
