namespace ScreenDrafts.Api.Contracts.Drafts.Requests;

public sealed record PickDecisionRequest(
       string Decision,
       string UserId);