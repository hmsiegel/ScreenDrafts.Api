namespace ScreenDrafts.Api.Application.Movies.Commands.CreateMovieFromImdb;
internal sealed class CreateMovieFromImdbCommandHandler : ICommandHandler<CreateMovieFromImdbCommand>
{
    private readonly IMovieRepository _movieRepository;

    public CreateMovieFromImdbCommandHandler(IMovieRepository movieRepository) => _movieRepository = movieRepository;

    public Task<Result> Handle(CreateMovieFromImdbCommand request, CancellationToken cancellationToken)
    {
        var imdbTitle = request.TitleData;

        var imdbUrl = $"https://www.imdb.com/title/{imdbTitle.Id}";

        var movie = Movie.Create(
            imdbTitle.Title,
            imdbTitle.Year,
            imdbTitle.Image,
            imdbUrl);

        _movieRepository.Add(movie);

        return Task.FromResult(Result.Success());
    }
}
