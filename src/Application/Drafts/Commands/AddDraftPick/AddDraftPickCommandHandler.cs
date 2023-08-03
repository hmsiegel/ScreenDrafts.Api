namespace ScreenDrafts.Api.Application.Drafts.Commands.AddMovie;
internal sealed class AddDraftPickCommandHandler : ICommandHandler<AddDraftPickCommand>
{
    private readonly IDraftRepository _draftRepository;
    private readonly IMovieRepository _movieRepository;

    public AddDraftPickCommandHandler(
        IDraftRepository draftRepository,
        IMovieRepository movieRepository)
    {
        _draftRepository = draftRepository;
        _movieRepository = movieRepository;
    }

    public async Task<Result> Handle(AddDraftPickCommand request, CancellationToken cancellationToken)
    {
        // Get the draft
        var draft = await _draftRepository.GetByIdAsync(request.DraftId, cancellationToken);

        if (draft is null)
        {
            return Result.Failure<Draft>(DomainErrors.Draft.NotFound);
        }

        // Get the movie
        var movie = await _movieRepository.GetByIdAsync(request.MovieId, cancellationToken);

        if (movie is null)
        {
            return Result.Failure<Movie>(DomainErrors.Movie.NotFound);
        }

        // Create the pick
        var pick = Pick.Create(request.DraftPosition);

        var drafterId = draft.DrafterIds!
            .Where(d => d.Value == request.DrafterId)!
            .FirstOrDefault();

        // Create the pick decision
        var pickDecision = PickDecision.Create(
            drafterId!,
            movie.Id!);

        // Add Pick Decision
        pick.AddPickDecision(pickDecision);

        draft.AddPick(pick);

        return Result.Success();
    }
}
