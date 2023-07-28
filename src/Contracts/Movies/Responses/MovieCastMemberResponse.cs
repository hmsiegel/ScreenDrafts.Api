namespace ScreenDrafts.Api.Contracts.Movies.Responses;

public sealed record MovieCastMemberResponse(
    DefaultIdType CastMemberId,
    string Name,
    string ImdbId,
    string RoleDescription);
