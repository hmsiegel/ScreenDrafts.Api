namespace ScreenDrafts.Api.Domain.Identity.Entities;
public sealed class Drafter : Entity<DrafterId>, IAuditableEntity
{
    private Drafter(
        DrafterId id,
        ApplicationUser user,
        string userId,
        bool hasRolloverVeto = false,
        bool hasRolloverVetooverride = false)
        : base(id)
    {
        User = user;
        UserId = userId;
        HasRolloverVeto = hasRolloverVeto;
        HasRolloverVetooverride = hasRolloverVetooverride;
    }

    private Drafter()
    {
    }

    public ApplicationUser? User { get; set; }
    public string? UserId { get; set; }
    public bool? HasRolloverVeto { get; private set; }
    public bool? HasRolloverVetooverride { get; private set; }

    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DefaultIdType CreatedBy { get; set; }
    public DefaultIdType? ModifiedBy { get; set; }

    public static Drafter Create(ApplicationUser user, string userId)
    {
        return new Drafter(
            DrafterId.CreateUnique(),
            user,
            userId);
    }

    public void AddRolloverVeto() => HasRolloverVeto = true;

    public void RemoveRolloverVeto() => HasRolloverVeto = false;

    public void AddRolloverVetooverride() => HasRolloverVetooverride = true;

    public void RemoveRolloverVetooverride() => HasRolloverVetooverride = false;
}
