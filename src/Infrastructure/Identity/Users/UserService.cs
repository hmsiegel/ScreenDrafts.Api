namespace ScreenDrafts.Api.Infrastructure.Identity.Users;
internal sealed partial class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IFileStorageService _fileStorage;
    private readonly IEventPublisher _events;
    private readonly ApplicationDbContext _dbContext;

    public UserService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        SignInManager<ApplicationUser> signInManager,
        IFileStorageService fileStorage,
        IEventPublisher events,
        ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _fileStorage = fileStorage;
        _events = events;
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsWithEmailAsync(string email, string? exceptId = null)
    {
        return await _userManager.FindByEmailAsync(email.Normalize()) is ApplicationUser user && user.Id.ToString() != exceptId;
    }

    public async Task<bool> ExistsWithNameAsync(string name)
    {
        return await _userManager.FindByNameAsync(name) is not null;
    }

    public async Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber, string? exceptId = null)
    {
        return await _userManager.Users
            .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber) is ApplicationUser user && user.Id.ToString() != exceptId;
    }

    public async Task<UserDetailsResponse> GetAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .AsNoTracking()
            .Where(u => u.Id.ToString() == userId)
            .FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException("User not found.");

        return user.Adapt<UserDetailsResponse>();
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
}
