namespace ScreenDrafts.Api.Contracts.Drafts.Responses;

public sealed record PickResponse(
    DefaultIdType Id,
    int DraftPosition,
    List<PickDecisionResponse> PickDecisions);