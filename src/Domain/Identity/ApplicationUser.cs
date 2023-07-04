namespace ScreenDrafts.Api.Domain.Identity;
public class ApplicationUser : IdentityUser< DefaultIdType>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public bool IsActive { get; set; }
    public Drafter? Drafter { get; set; }
    public Host? Host { get; set; }
    public string ObjectId { get; set; } = default!;
}
