namespace ScreenDrafts.Api.Contracts.Drafts.Responses;
public sealed record DraftResponse(
    DefaultIdType Id,
    string Name,
    string DraftType,
    DateTime ReleasDate,
    int Runtime,
    string EpisodeNumber,
    List<DrafterResponse> Drafters);
