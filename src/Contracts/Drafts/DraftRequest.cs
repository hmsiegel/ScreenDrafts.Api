namespace ScreenDrafts.Api.Contracts.Drafts;
public sealed record DraftRequest(
    string Name,
    int DraftType,
    int NumberOfDrafters);
