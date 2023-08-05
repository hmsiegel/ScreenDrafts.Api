namespace ScreenDrafts.Api.Persistence.Seeders;
internal sealed class HostSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DrafterSeeder> _logger;
    private readonly IHostRepository _host;
    private readonly ICsvFileService _csv;
    private readonly IUserService _userService;

    public HostSeeder(
        ApplicationDbContext context,
        ILogger<DrafterSeeder> logger,
        IHostRepository host,
        ICsvFileService csv,
        IUserService userService)
    {
        _context = context;
        _logger = logger;
        _host = host;
        _csv = csv;
        _userService = userService;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        if (_context.Users is not null)
        {
            string path = ProjectSourcePath.Value;
            var records = _csv.ReadCsvFile<CreateUserRequest>(path + "Seeders\\Files\\hosts.csv");

            var existingHosts = await _host.GetAllHosts(cancellationToken);
            var existingHostUserIds = existingHosts.ConvertAll(u => u.UserId);

            foreach (var record in records)
            {
                var existingUser = await _userService.GetByFirstAndLastNameAsync(
                    record.FirstName,
                    record.LastName,
                    cancellationToken);

                if (existingUser is null)
                {
                    var newUserId = await _userService.CreateAsync(record);
                    existingUser = await _userService.GetAsync(newUserId, cancellationToken);
                }

                if (!existingHostUserIds.Contains(existingUser.Id))
                {
                    var host = Host.Create(existingUser.Id);

                    _logger.LogInformation("Seeding host");
                    _host.Add(host);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded hosts successfully");
        }
    }
}
