namespace ScreenDrafts.Api.Persistence.Initialization;
internal sealed class CustomSeederRunner
{
    private readonly ICustomSeeder[] _seeders;

    public CustomSeederRunner(IServiceProvider serviceProvider)
    {
        _seeders = serviceProvider.GetServices<ICustomSeeder>().ToArray();
    }

    public async Task RunSeedersAsync(CancellationToken cancellationToken = default)
    {
        foreach (var seeder in _seeders)
        {
            await seeder.InitializeAsync(cancellationToken);
        }
    }
}
