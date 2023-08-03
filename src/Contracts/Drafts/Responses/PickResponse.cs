namespace ScreenDrafts.Api.Contracts.Drafts.Responses;

public sealed record PickResponse(
    string Id,
    int DraftPosition,
    List<PickDecisionResponse> PickDecisions);