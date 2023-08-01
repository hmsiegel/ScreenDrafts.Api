namespace ScreenDrafts.Api.Contracts.Drafts.Requests;

public sealed record BlessingDecisionRequest(
       string DrafterId,
       string BlessingUsed);