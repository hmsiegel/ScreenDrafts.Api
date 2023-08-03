namespace ScreenDrafts.Api.Application.Common.Services;
internal sealed class CastAndCrewService : ICastAndCrewService
{
    private readonly ICastMemberRepository _castMemberRepository;
    private readonly ICrewMemberRepository _crewMemberRepository;
    private readonly IMovieRepository _movieRepository;

    public CastAndCrewService(
        ICastMemberRepository castMemberRepository,
        ICrewMemberRepository crewMemberRepository,
        IMovieRepository movieRepository)
    {
        _castMemberRepository = castMemberRepository;
        _crewMemberRepository = crewMemberRepository;
        _movieRepository = movieRepository;
    }

    public List<MovieCastMemberResponse> GetCastMembers(Movie? movie)
    {
        return _movieRepository.GetAllMovieCastMembers(movie!.Id!.Value)
                        .Result
                        .ConvertAll(x => new MovieCastMemberResponse(
                            x.CastMemberId.Value,
                            _castMemberRepository.GetByCastMemberIdAsync(x.CastMemberId.Value).Result.Name!,
                            _castMemberRepository.GetByCastMemberIdAsync(x.CastMemberId.Value).Result.ImdbId!,
                            x.RoleDescription!));
    }

    public List<MovieCrewMemberResponse> GetCrewMembers(Movie? movie)
    {
        return _movieRepository.GetAllMovieCrewMembers(movie!.Id!.Value)
                        .Result
                        .ConvertAll(x => new MovieCrewMemberResponse(
                        x.CrewMemberId.Value,
                        _crewMemberRepository.GetByCrewMemberIdAsync(x.CrewMemberId.Value).Result.Name!,
                        _crewMemberRepository.GetByCrewMemberIdAsync(x.CrewMemberId.Value).Result.ImdbId!,
                        x.JobDescription!));
    }
}
