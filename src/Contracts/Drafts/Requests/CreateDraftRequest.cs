namespace ScreenDrafts.Api.Contracts.Drafts.Requests;
public sealed record CreateDraftRequest(
    string Name,
    string DraftType,
    int NumberOfDrafters);
