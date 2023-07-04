namespace ScreenDrafts.Api.Persistence.Initialization;
internal interface IDatabaseInitializer
{
    Task InitializeDatabaseAsync(CancellationToken cancellationToken);
}
