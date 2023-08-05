namespace ScreenDrafts.Api.Persistence.Identity.Users;
internal sealed partial class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IFileStorageService _fileStorage;
    private readonly IEventPublisher _events;
    private readonly ApplicationDbContext _dbContext;
    private readonly ICacheService _cache;
    private readonly ICacheKeyService _cacheKey;
    private readonly SecuritySettings _securitySettings;
    private readonly IEmailTemplateService _templateService;
    private readonly IMailService _mailService;
    private readonly IJobService _jobService;

    public UserService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        SignInManager<ApplicationUser> signInManager,
        IFileStorageService fileStorage,
        IEventPublisher events,
        ApplicationDbContext dbContext,
        ICacheService cache,
        ICacheKeyService cacheKey,
        IOptions<SecuritySettings> securitySettings,
        IEmailTemplateService templateService,
        IMailService mailService,
        IJobService jobService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _fileStorage = fileStorage;
        _events = events;
        _dbContext = dbContext;
        _cache = cache;
        _cacheKey = cacheKey;
        _securitySettings = securitySettings.Value;
        _templateService = templateService;
        _mailService = mailService;
        _jobService = jobService;
    }

    public async Task<bool> ExistsWithEmailAsync(string email, string? exceptId = null)
    {
        return await _userManager.FindByEmailAsync(email.Normalize()) is ApplicationUser user && user.Id != exceptId;
    }

    public async Task<bool> ExistsWithNameAsync(string name)
    {
        return await _userManager.FindByNameAsync(name) is not null;
    }

    public async Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber, string? exceptId = null)
    {
        return await _userManager.Users
            .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber) is ApplicationUser user && user.Id != exceptId;
    }

    public async Task<UserDetailsResponse> GetByIdAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException("User not found.");

        return user.Adapt<UserDetailsResponse>();
    }

    public async Task<ApplicationUser> GetAsync(string userId, CancellationToken cancellationToken)
    {
        return await _userManager.Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<UserDetailsResponse>> GetListAsync(CancellationToken cancellationToken)
    {
        return (await _userManager.Users
            .AsNoTracking()
            .ToListAsync(cancellationToken))
        .Adapt<List<UserDetailsResponse>>();
    }

    public async Task<PaginationResponse<UserDetailsResponse>> SearchAsync(UserListFilter filter, CancellationToken cancellationToken)
    {
        var spec = new EntitiesByPaginationFilterSpecification<ApplicationUser>(filter);

        var users = await _userManager.Users
            .WithSpecification(spec)
            .ProjectToType<UserDetailsResponse>()
            .ToListAsync(cancellationToken);

        int count = await _userManager.Users
            .CountAsync(cancellationToken);

        return new PaginationResponse<UserDetailsResponse>(users, count, filter.PageNumber, filter.PageSize);
    }

    public async Task<ApplicationUser> GetByFirstAndLastNameAsync(string firstName, string lastName, CancellationToken cancellationToken)
    {
        string formattedFirstName = firstName.Replace(".", string.Empty, StringComparison.InvariantCultureIgnoreCase);
        string formattedLastName = lastName.Replace(".", string.Empty, StringComparison.InvariantCultureIgnoreCase);

        var users = await _userManager.Users.ToListAsync(cancellationToken);

        return users.Find(u => u.FirstName!.Replace(
            ".",
            string.Empty,
            StringComparison.InvariantCultureIgnoreCase) == formattedFirstName
        && u.LastName == formattedLastName);
    }
}
