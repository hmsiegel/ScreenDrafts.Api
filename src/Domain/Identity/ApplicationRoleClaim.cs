namespace ScreenDrafts.Api.Domain.Identity;
public class ApplicationRoleClaim : IdentityRoleClaim<DefaultIdType>
{
    public string? CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
}
