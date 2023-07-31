namespace ScreenDrafts.Api.Contracts.Drafts.Responses;
public sealed record BasicDraftResponse(
    DefaultIdType Id,
    string Name,
    string DraftType,
    DateTime ReleaseDate,
    int Runtime,
    string EpisodeNumber);
