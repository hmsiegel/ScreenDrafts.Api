namespace ScreenDrafts.Api.Domain.Identity.Entities;
public sealed class Host : Entity<HostId>, IAuditableEntity
{
    private Host(
        HostId id,
        string userId,
        int predictionPoints = 0)
        : base(id)
    {
        UserId = userId;
        PredictionPoints = predictionPoints;
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

    public static Host Create(string userId)
    {
        return new Host(
            HostId.CreateUnique(),
            userId,
            0);
    }

    public void AddPredictionPoints(int predictionPoints) => PredictionPoints += predictionPoints;

    public void ResetPredictionPoints() => PredictionPoints = 0;
}
