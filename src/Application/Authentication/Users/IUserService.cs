namespace ScreenDrafts.Api.Application.Authentication.Users;
public interface IUserService
{
    Task<bool> ExistsWithNameAsync(string name);
    Task<bool> ExistsWithEmailAsync(string email, string? exceptId = null);
    Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber, string? exceptId = null);
    Task<string> GetOrCreateFromPrincipalAsync(ClaimsPrincipal principal);
    Task<string> CreateAsync(RegisterRequest request, string origin);
    Task UpdateAsync(UpdateUserRequest request, string userId);
    Task<List<UserDetailsResponse>> GetListAsync(CancellationToken cancellationToken);
    Task<UserDetailsResponse> GetAsync(string userId, CancellationToken cancellationToken);
}
