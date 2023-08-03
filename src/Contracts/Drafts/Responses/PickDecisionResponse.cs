namespace ScreenDrafts.Api.Contracts.Drafts.Responses;

public sealed record PickDecisionResponse(
    string Id,
    DrafterResponse Drafter,
    MovieResponse Movie,
    List<BlessingDecisionResponse>? BlessingDecisions);
