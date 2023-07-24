namespace ScreenDrafts.Api.Contracts.Drafts.Requests;
public sealed record UpdateDraftRequest(
    string Name,
    string DraftType,
    DateTime ReleaseDate,
    int Runtime,
    string EpisodeNumber,
    int NumberOfDrafters,
    int NumberOfFilms);
