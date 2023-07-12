namespace ScreenDrafts.Api.Domain.DraftAggregate.Entities;
public sealed class SelectedMovie : Entity, IAuditableEntity
{
    private SelectedMovie(
        string movieId,
        int draftPosition,
        string drafterId)
    {
        MovieId = movieId;
        DraftPosition = draftPosition;
        DrafterId = drafterId;
    }

    private SelectedMovie()
    {
    }

    public string MovieId { get; private set; }
    public Movie Movie { get; private set; }
    public int DraftPosition { get; private set; }
    public string DrafterId { get; private set; }
    public Drafter Drafter { get; private set; }
    public bool IsVetoed { get; private set; }
    public string DrafterWhoPlayedVetoId { get; private set; }
    public Drafter DrafterWhoPlayedVeto { get; private set; }
    public bool WasVetoOverride { get; private set; }
    public string DrafterWhoPlayedVetoOverrideId { get; private set; }
    public Drafter DrafterWhoPlayedVetoOverride { get; private set; }
    public bool WasCommissonerOverride { get; private set; }

    public DefaultIdType CreatedBy { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DefaultIdType? ModifiedBy { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static SelectedMovie Create(
        string movieId,
        int draftPosition,
        string drafterId)
    {
        return new SelectedMovie(
            movieId,
            draftPosition,
            drafterId);
    }

    public void Veto(string drafterId)
    {
        IsVetoed = true;
        DrafterWhoPlayedVetoId = drafterId;
    }

    public void VetoOverride(string drafterId)
    {
        WasVetoOverride = true;
        DrafterWhoPlayedVetoOverrideId = drafterId;
    }

    public void CommissoerOverride(string drafterId)
    {
        WasCommissonerOverride = true;
        DrafterWhoPlayedVetoOverrideId = drafterId;
    }
}
