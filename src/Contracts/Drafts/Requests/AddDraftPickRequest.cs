namespace ScreenDrafts.Api.Contracts.Drafts.Requests;
public sealed record AddDraftPickRequest(
    int DraftPosition,
    string DrafterId,
    string MovieId);
