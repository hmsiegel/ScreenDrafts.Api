namespace ScreenDrafts.Api.Persistence.Seeders;
internal sealed class UserSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UserSeeder> _logger;
    private readonly ICsvFileService _csv;
    private readonly IUserService _userService;

    public UserSeeder(
        ApplicationDbContext context,
        ILogger<UserSeeder> logger,
        ICsvFileService csv,
        IUserService userService)
    {
        _context = context;
        _logger = logger;
        _csv = csv;
        _userService = userService;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        string path = ProjectSourcePath.Value;

        if (!_context.Users.Any())
        {
            _logger.LogInformation("Starting to seed users");

            var userData = _csv.ReadCsvFile<CreateUserRequest>(path + "Seeders\\Files\\users.csv");

            if (userData is not null)
            {
                foreach (var user in userData)
                {
                    await _userService.CreateAsync(user);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded users successfully");
        }
    }
}
