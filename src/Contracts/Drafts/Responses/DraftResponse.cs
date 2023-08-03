namespace ScreenDrafts.Api.Contracts.Drafts.Responses;
public sealed record DraftResponse(
    string Id,
    string Name,
    string DraftType,
    string ReleaseDate,
    string Runtime,
    int EpisodeNumber,
    List<HostResponse> Hosts,
    List<PickResponse> Picks);
