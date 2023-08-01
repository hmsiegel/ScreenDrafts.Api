namespace ScreenDrafts.Api.Contracts.Drafts.Responses;

public sealed record PickDecisionResponse(
    DefaultIdType Id,
    MovieResponse Movie,
    List<BlessingDecisionResponse>? BlessingDecisions);
