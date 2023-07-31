namespace ScreenDrafts.Api.Contracts.Drafts.Responses;
public sealed record FullDraftResponse(
    DefaultIdType Id,
    string Name,
    string DraftType,
    DateTime ReleaseDate,
    int Runtime,
    string EpisodeNumber,
    List<HostResponse> Hosts,
    List<DrafterResponse> Drafters,
    List<SelectedMovieResponse> SelectedMovies);
