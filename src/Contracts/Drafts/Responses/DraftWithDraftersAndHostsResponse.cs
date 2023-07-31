namespace ScreenDrafts.Api.Contracts.Drafts.Responses;
public sealed record DraftWithDraftersAndHostsResponse(
    DefaultIdType Id,
    string Name,
    string DraftType,
    DateTime ReleaseDate,
    int Runtime,
    string EpisodeNumber,
    List<HostResponse> Hosts,
    List<DrafterResponse> Drafters);
