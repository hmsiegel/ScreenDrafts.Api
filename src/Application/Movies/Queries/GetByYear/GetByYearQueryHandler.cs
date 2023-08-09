namespace ScreenDrafts.Api.Application.Movies.Queries.GetByYear;
internal sealed class GetByYearQueryHandler : IQueryHandler<GetByYearQuery, List<MovieResult>>
{
    private readonly IMovieRepository _movieRepository;

    public GetByYearQueryHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Result<List<MovieResult>>> Handle(GetByYearQuery request, CancellationToken cancellationToken)
    {
        var movies = await _movieRepository.GetMoviesByYearAsync(request.Year, cancellationToken);

        return movies.ConvertAll(x => new MovieResult(
            x.Id!.Value.ToString(),
            x.Title!,
            x.Year!,
            x.ImdbUrl!.GetLastUrlSegment()));
    }
}
