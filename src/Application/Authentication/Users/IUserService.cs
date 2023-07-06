namespace ScreenDrafts.Api.Application.Authentication.Users;
public interface IUserService
{
    // Generic
    Task<PaginationResponse<UserDetailsResponse>> SearchAsync(UserListFilter filter, CancellationToken cancellationToken);
    Task<bool> ExistsWithNameAsync(string name);
    Task<bool> ExistsWithEmailAsync(string email, string? exceptId = null);
    Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber, string? exceptId = null);
    Task<List<UserDetailsResponse>> GetListAsync(CancellationToken cancellationToken);
    Task<UserDetailsResponse> GetAsync(string userId, CancellationToken cancellationToken);

    // Create/ Update
    Task<string> GetOrCreateFromPrincipalAsync(ClaimsPrincipal principal);
    Task<string> CreateAsync(RegisterRequest request, string origin);
    Task UpdateAsync(UpdateUserRequest request, string userId);
    Task ToggleStatusAsync(ToggleUserStatusRequest request, CancellationToken cancellationToken);

    // Confirm
    Task<string> GetEmailVerificationUriAsync(ApplicationUser user, string origin);
    Task<string> ConfirmEmailAsync(string userId, string code, CancellationToken cancellationToken);
    Task<string> ConfirmPhoneNumberAsync(string userId, string code);

    // Password
    Task<string> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
    Task<string> ResetPasswordAsync(ResetPasswordRequest request, string origin);
    Task ChangePasswordAsync(ChangePasswordRequest request, string userId);

    // Permissions
    Task<List<string>> GetPermissionsAsync(string userId, CancellationToken cancellationToken);
    Task<bool> HasPermissionAsync(string userId, string permission, CancellationToken cancellationToken);
    Task InvalidatePermissionCacheAsync(string userId, CancellationToken cancellationToken);

    // Roles
    Task<List<UserRoleResponse>> GetRolesAsync(string userId, CancellationToken cancellationToken);
    Task<string> AssignRolesAsync(string userId, UserRolesRequest request, CancellationToken cancellationToken);
}
