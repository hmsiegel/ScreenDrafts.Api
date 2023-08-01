namespace ScreenDrafts.Api.Contracts.Drafts.Requests;

public sealed record AddBlessingDecisionRequest(
       string DrafterId,
       string MovieId,
       string BlessingUsed);