namespace ScreenDrafts.Api.Application.Drafts.Commands.AddMovie;
internal sealed class AddMovieCommandHandler : ICommandHandler<AddMovieCommand>
{
    private readonly IDraftRepository _draftRepository;
    private readonly IMovieRepository _movieRepository;

    public AddMovieCommandHandler(
        IDraftRepository draftRepository,
        IMovieRepository movieRepository)
    {
        _draftRepository = draftRepository;
        _movieRepository = movieRepository;
    }

    public async Task<Result> Handle(AddMovieCommand request, CancellationToken cancellationToken)
    {
        // Get the draft
        var draft = await _draftRepository.GetByIdAsync(request.DraftId, cancellationToken);

        if (draft is null)
        {
            return Result.Failure<Draft>(DomainErrors.Draft.NotFound);
        }

        // Get the movie
        var movie = await _movieRepository.GetByIdAsync(request.MovieId);

        if (movie is null)
        {
            return Result.Failure<Movie>(DomainErrors.Movie.NotFound);
        }

        // Create the selected movie
        var selectedMovie = SelectedMovie.Create(
            movie.Id!,
            request.DraftPosition);

        selectedMovie.AddPickDecision(
            request.PickDecision.UserId,
            request.PickDecision.Decision);

        draft.AddSelectedMovie(selectedMovie);

        return Result.Success();
    }
}
