namespace ScreenDrafts.Api.Domain.Authentication;
public sealed class Role : Enumeration<Role>
{
    public Role(int id, string? name)
        : base(id, name)
    {
    }
}
