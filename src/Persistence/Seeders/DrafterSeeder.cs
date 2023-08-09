namespace ScreenDrafts.Api.Persistence.Seeders;
internal sealed class DrafterSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DrafterSeeder> _logger;
    private readonly IDrafterRepository _drafterRepository;

    public DrafterSeeder(
        ApplicationDbContext context,
        ILogger<DrafterSeeder> logger,
        IDrafterRepository drafterRepository)
    {
        _context = context;
        _logger = logger;
        _drafterRepository = drafterRepository;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        if (!_context.Users.Any())
        {
            var existingDrafters = await _drafterRepository.GetAllDrafters(cancellationToken);
            var existingDrafterUserIds = existingDrafters.ConvertAll(d => d.UserId);

            foreach (var user in _context.Users)
            {
                if (!existingDrafterUserIds.Contains(user.Id))
                {
                    _logger.LogInformation("Seeding drafter");
                    var newDrafter = Drafter.Create(user.Id);
                    _drafterRepository.Add(newDrafter);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded drafters successfully");
        }
    }
}
