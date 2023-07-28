namespace ScreenDrafts.Api.Contracts.Movies.Responses;
public sealed record MovieResponse(
    DefaultIdType Id,
    string Title,
    string Year,
    string ImageUrl,
    string ImdbUrl,
    List<MovieCrewMemberResponse> Crew,
    List<MovieCastMemberResponse> Cast);
