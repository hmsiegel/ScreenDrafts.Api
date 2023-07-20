namespace ScreenDrafts.Api.Domain.Identity.Entities;
public sealed class Host : Entity<HostId>, IAuditableEntity
{
    private Host(
        HostId id,
        ApplicationUser user,
        string userId)
        : base(id)
    {
        User = user;
        UserId = userId;
    }

    private Host()
    {
    }

    public ApplicationUser? User { get; set; }
    public string? UserId { get; set; }
    public int? PredictionPoints { get; private set; }

    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DefaultIdType CreatedBy { get; set; }
    public DefaultIdType? ModifiedBy { get; set; }

    public static Host Create(ApplicationUser user, string userId)
    {
        return new Host(
            HostId.CreateUnique(),
            user,
            userId);
    }

    public void AddPredictionPoints(int predictionPoints) => PredictionPoints += predictionPoints;

    public void ResetPredictionPoints() => PredictionPoints = 0;
}
