namespace ScreenDrafts.Api.Application.Common.Services;

internal interface ICastAndCrewService
{
    List<MovieCastMemberResponse> GetCastMembers(Movie? movie);
    List<MovieCrewMemberResponse> GetCrewMembers(Movie? movie);
}