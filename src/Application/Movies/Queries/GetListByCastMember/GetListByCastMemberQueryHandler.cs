namespace ScreenDrafts.Api.Application.Movies.Queries.GetListByCastMember;
public sealed class GetListByCastMemberQueryHandler : IQueryHandler<GetListByCastMemberQuery, List<MovieResult>>
{
    private readonly IMovieRepository _movieRepository;
    private readonly ICastMemberRepository _castMemberRepository;

    public GetListByCastMemberQueryHandler(IMovieRepository movieRepository, ICastMemberRepository castMemberRepository)
    {
        _movieRepository = movieRepository;
        _castMemberRepository = castMemberRepository;
    }

    public async Task<Result<List<MovieResult>>> Handle(GetListByCastMemberQuery request, CancellationToken cancellationToken)
    {
        var castMember = await _castMemberRepository.GetByImdbIdAsync(request.ImdbId, cancellationToken);

        var movies = await _movieRepository.GetMoviesWithCastMember(castMember.Id!.Value, cancellationToken);

        return movies.ConvertAll(x => new MovieResult(
            x.Id!.Value.ToString(),
            x.Title!,
            x.Year!,
            x.ImdbUrl!.GetLastUrlSegment()));
    }
}
