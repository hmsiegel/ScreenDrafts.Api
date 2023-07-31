namespace ScreenDrafts.Api.Contracts.Drafts.Requests;
public sealed record AddMovieRequest(
    string MovieId,
    int DraftPosition,
    PickDecisionRequest PickDecision);
