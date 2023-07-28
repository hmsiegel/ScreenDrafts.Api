namespace ScreenDrafts.Api.Application.Movies.Commands.CreateMovieFromImdb;
internal sealed class CreateMovieFromImdbCommandHandler : ICommandHandler<CreateMovieFromImdbCommand>
{
    private readonly IImdbService _imdbService;
    private readonly IMovieRepository _movieRepository;

    public CreateMovieFromImdbCommandHandler(
        IImdbService imdbService,
        IMovieRepository movieRepository)
    {
        _imdbService = imdbService;
        _movieRepository = movieRepository;
    }

    public async Task<Result> Handle(CreateMovieFromImdbCommand request, CancellationToken cancellationToken)
    {
        var imdbTitle = await _imdbService.GetMovieInformation(request.ImdbId, TitleOptions.FullCast);

        var imdbUrl = $"https://www.imdb.com/title/{request.ImdbId}";

        var movie = Movie.Create(
            imdbTitle.Title,
            imdbTitle.Year,
            imdbTitle.Image,
            imdbUrl);

        _movieRepository.Add(movie);

        return Result.Success();
    }
}
