namespace ScreenDrafts.Api.Domain.HostEntitty;
public sealed class Host : Entity, IAuditableEntity
{
    private Host(
        DefaultIdType id)
        : base(id)
    {
    }

    public ApplicationUser? User { get; set; }
    public int PredictionPoints { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static Host Create(DefaultIdType id)
    {
        return new Host(id);
    }

    public void AddPredictionPoints(int predictionPoints) => PredictionPoints += predictionPoints;

    public void ResetPredictionPoints() => PredictionPoints = 0;
}
