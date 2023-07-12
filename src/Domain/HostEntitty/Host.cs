namespace ScreenDrafts.Api.Domain.HostEntitty;
public sealed class Host : Entity, IAuditableEntity
{
    private Host(
        string id)
        : base(id)
    {
    }

    public ApplicationUser? User { get; set; }
    public string? UserId { get; set; }
    public int? PredictionPoints { get; private set; }
    public string? DraftId { get; private set; }
    public Draft? Draft { get; private set; }

    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DefaultIdType CreatedBy { get; set; }
    public DefaultIdType? ModifiedBy { get; set; }

    public static Host Create(string id)
    {
        return new Host(id);
    }

    public void AddPredictionPoints(int predictionPoints) => PredictionPoints += predictionPoints;

    public void ResetPredictionPoints() => PredictionPoints = 0;
}
