namespace ScreenDrafts.Api.Contracts.Drafts.Responses;

public sealed record SelectedMovieResponse(DefaultIdType Id, DefaultIdType MovieId, int DraftPosition, List<PickDecisionResponse> PickDecisions);
