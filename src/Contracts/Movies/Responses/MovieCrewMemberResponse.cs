namespace ScreenDrafts.Api.Contracts.Movies.Responses;

public sealed record MovieCrewMemberResponse(
    DefaultIdType CrewMemberId,
    string Name,
    string ImdbId,
    string JobDescription);