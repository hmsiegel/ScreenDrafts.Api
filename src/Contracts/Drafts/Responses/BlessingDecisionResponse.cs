namespace ScreenDrafts.Api.Contracts.Drafts.Responses;

public sealed record BlessingDecisionResponse(
    DrafterResponse Drafter,
    string BlessingUsed);