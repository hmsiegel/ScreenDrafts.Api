namespace ScreenDrafts.Api.Application.Drafts.Queries.GetAllDrafts;
internal sealed class GetAllDraftsQueryHandler : IQueryHandler<GetAllDraftsQuery, List<FullDraftResponse>>
{
    private readonly IDraftRepository _draftRepository;
    private readonly IDrafterRepository _drafterRepository;
    private readonly IUserService _userService;
    private readonly IHostRepository _hostRepository;

    public GetAllDraftsQueryHandler(
        IDraftRepository draftRepository,
        IDrafterRepository drafterRepository,
        IUserService userService,
        IHostRepository hostRepository)
    {
        _draftRepository = draftRepository;
        _drafterRepository = drafterRepository;
        _userService = userService;
        _hostRepository = hostRepository;
    }

    public async Task<Result<List<FullDraftResponse>>> Handle(GetAllDraftsQuery request, CancellationToken cancellationToken)
    {
        var result = new List<FullDraftResponse>();
        var drafts = await _draftRepository.GetAllDrafts(cancellationToken);

        foreach (var draft in drafts)
        {
            var drafters = new List<DrafterResponse>();
            var hosts = new List<HostResponse>();
            var selectedMovies = new List<SelectedMovieResponse>();

            foreach (var drafterId in draft.DrafterIds!)
            {
                var drafter = await _drafterRepository.GetByDrafterIdAsync(drafterId.Value, cancellationToken);

                var user = await _userService.GetByIdAsync(drafter!.UserId!, cancellationToken);

                if (drafter is null)
                {
                    return Result.Failure<List<FullDraftResponse>>(DomainErrors.Drafter.NotFound);
                }

                drafters.Add(new DrafterResponse(
                    drafter.Id!.Value,
                    user.FirstName,
                    user.LastName,
                    (bool)drafter.HasRolloverVeto!,
                    (bool)drafter.HasRolloverVetooverride!));
            }

            foreach (var hostId in draft.HostIds!)
            {
                var host = await _hostRepository.GetByHostIdAsync(hostId.Value, cancellationToken);

                var user = await _userService.GetByIdAsync(host!.UserId!, cancellationToken);

                if (host is null)
                {
                    return Result.Failure<List<FullDraftResponse>>(DomainErrors.Drafter.NotFound);
                }

                hosts.Add(new HostResponse(
                    host.Id!.Value,
                    user.FirstName,
                    user.LastName,
                    host.PredictionPoints!.Value));
            }

            foreach (var selectedMovie in draft.SelectedMovies!)
            {
                var pickDecisions = new List<PickDecisionResponse>();

                foreach (var movieDecision in selectedMovie.PickDecisions)
                {
                    var pickDecision = new PickDecisionResponse(
                        movieDecision.Decision.Name,
                        movieDecision.UserId);

                    pickDecisions.Add(pickDecision);
                }

                selectedMovies.Add(new SelectedMovieResponse(
                    selectedMovie.Id!.Value,
                    selectedMovie.MovieId!.Value,
                    selectedMovie.DraftPosition!,
                    pickDecisions));
            }

            var fullDraft = new FullDraftResponse(
                draft.Id!.Value,
                draft.Name!,
                draft.DraftType!.Name!,
                (DateTime)draft.ReleaseDate!,
                (int)draft.Runtime!,
                draft.EpisodeNumber!,
                hosts,
                drafters,
                selectedMovies);

            result.Add(fullDraft);
        }

        return Result.Success(result);
    }
}
