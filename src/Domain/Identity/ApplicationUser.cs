namespace ScreenDrafts.Api.Domain.Identity;
public class ApplicationUser : IdentityUser< DefaultIdType>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public Drafter? Drafter { get; set; }
    public Host? Host { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string? ObjectId { get; set; }
}
