namespace ScreenDrafts.Api.Application.Authentication.Users;
public sealed class UserListFilter : PaginationFilter
{
    public bool? IsActive { get; set; }
}
