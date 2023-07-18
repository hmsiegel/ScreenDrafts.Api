namespace ScreenDrafts.Api.Contracts.Drafts;
public sealed record CreateDraftRequest(
    string Name,
    string DraftType,
    int NumberOfDrafters);
