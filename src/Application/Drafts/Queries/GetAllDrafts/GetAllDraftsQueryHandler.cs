namespace ScreenDrafts.Api.Application.Drafts.Queries.GetAllDrafts;
internal sealed class GetAllDraftsQueryHandler : IQueryHandler<GetAllDraftsQuery, List<DraftResponse>>
{
    private readonly IDraftRepository _draftRepository;
    private readonly IDrafterRepository _drafterRepository;
    private readonly IHostRepository _hostRepository;
    private readonly IUserService _userService;
    private readonly IMovieRepository _movieRepository;
    private readonly ICastAndCrewService _castAndCrewService;

    public GetAllDraftsQueryHandler(
        IDraftRepository draftRepository,
        IDrafterRepository drafterRepository,
        IHostRepository hostRepository,
        IUserService userService,
        IMovieRepository movieRepository,
        ICastAndCrewService castAndCrewService)
    {
        _draftRepository = draftRepository;
        _drafterRepository = drafterRepository;
        _hostRepository = hostRepository;
        _userService = userService;
        _movieRepository = movieRepository;
        _castAndCrewService = castAndCrewService;
    }

    public async Task<Result<List<DraftResponse>>> Handle(GetAllDraftsQuery request, CancellationToken cancellationToken)
    {
        var result = new List<DraftResponse>();
        var drafts = await _draftRepository.GetAllDrafts(cancellationToken);

        foreach (var draft in drafts)
        {
            var hosts = new List<HostResponse>();

            foreach (var hostId in draft.HostIds!)
            {
                var host = await _hostRepository.GetByHostIdAsync(hostId.Value, cancellationToken);

                if (host is null)
                {
                    return Result.Failure<List<DraftResponse>>(DomainErrors.Host.NotFound);
                }

                var user = await _userService.GetByIdAsync(host.UserId!, cancellationToken);

                hosts.Add(new HostResponse(
                    host.Id!.Value,
                    user.FirstName,
                    user.LastName,
                    (int)host.PredictionPoints!));
            }

            var draftPicks = new List<PickResponse>();
            var currentPicks = draft.Picks!.OrderByDescending(p => p.DraftPosition).ToList();
            var duplicatePicks = DuplicatedPicks(currentPicks);

            foreach (var draftPick in currentPicks.Except(duplicatePicks).ToList())
            {
                var pickDecisions = new List<PickDecisionResponse>();
                var pickedMovie = await _movieRepository.GetByIdAsync(draftPick.PickDecisions[0].MovieId.Value, cancellationToken);

                List<MovieCrewMemberResponse> movieCrewMembers = _castAndCrewService.GetCrewMembers(pickedMovie);
                List<MovieCastMemberResponse> movieCastMembers = _castAndCrewService.GetCastMembers(pickedMovie);
                var drafter = await _drafterRepository.GetByDrafterIdAsync(draftPick.PickDecisions[0].DrafterId!.Value, cancellationToken);
                var drafterUser = await _userService.GetByIdAsync(drafter.UserId!, cancellationToken);

                var pickDecision = new PickDecisionResponse(
                    draftPick.PickDecisions[0].Id!.Value!.ToString(),
                    new DrafterResponse(
                        drafter.Id!.Value,
                        drafterUser.FirstName,
                        drafterUser.LastName,
                        (bool)drafter.HasRolloverVeto!,
                        (bool)drafter.HasRolloverVetooverride!),
                    new MovieResponse(
                        pickedMovie.Id!.Value,
                        pickedMovie.Title!,
                        pickedMovie.Year!,
                        pickedMovie.ImageUrl!,
                        pickedMovie.ImdbUrl!,
                        movieCrewMembers,
                        movieCastMembers),
                    null);

                pickDecisions.Add(pickDecision);

                draftPicks.Add(new PickResponse(
                    draftPick.Id!.Value.ToString(),
                    draftPick.DraftPosition,
                    pickDecisions));
            }

            foreach (var pickBatch in duplicatePicks.GroupBy(x => x.DraftPosition).Chunk(3))
            {
                var pickDecisions = new List<PickDecisionResponse>();

                foreach (IGrouping<int, Pick>? similarPicks in pickBatch)
                {
                    var newPick = Pick.Create(similarPicks.Key);

                    foreach (var pick in similarPicks)
                    {
                        var drafterId = pick.PickDecisions[0].DrafterId;

                        var pickDecision = PickDecision.Create(
                            drafterId,
                            pick.PickDecisions[0].MovieId);

                        newPick.AddPickDecision(pickDecision);

                        if (pick.PickDecisions[0].BlessingDecisions!.Count != 0)
                        {
                            var blessingDecision = BlessingDecision.Create(
                                pick.PickDecisions[0].BlessingDecisions![0].DrafterId,
                                pick.PickDecisions[0].BlessingDecisions![0].BlessingUsed);

                            pickDecision.AddBlessingDecision(blessingDecision);
                        }
                    }

                    var newDraftPick = new PickResponse(
                        newPick.Id!.Value.ToString(),
                        newPick.DraftPosition,
                        pickDecisions);

                    foreach (var pickDecision in newPick.PickDecisions)
                    {
                        var pickedMovie = await _movieRepository.GetByIdAsync(pickDecision.MovieId.Value, cancellationToken);

                        var movieCrewMembers = _castAndCrewService.GetCrewMembers(pickedMovie);
                        var movieCastMembers = _castAndCrewService.GetCastMembers(pickedMovie);
                        var pickBlessings = new List<BlessingDecisionResponse>();

                        if (pickDecision.BlessingDecisions!.Count != 0)
                        {
                            foreach (var blessing in pickDecision.BlessingDecisions!)
                            {
                                var blessingDrafter = await _drafterRepository.GetByDrafterIdAsync(blessing.DrafterId!.Value, cancellationToken);
                                var blessingUser = await _userService.GetByIdAsync(blessingDrafter.UserId!, cancellationToken);

                                pickBlessings.Add(new BlessingDecisionResponse(
                                    new DrafterResponse(
                                        blessingDrafter.Id!.Value,
                                        blessingUser.FirstName,
                                        blessingUser.LastName,
                                        (bool)blessingDrafter.HasRolloverVeto!,
                                        (bool)blessingDrafter.HasRolloverVetooverride!),
                                    blessing.BlessingUsed!.Name));
                            }
                        }

                        var drafter = await _drafterRepository.GetByDrafterIdAsync(pickDecision.DrafterId!.Value, cancellationToken);
                        var drafterUser = await _userService.GetByIdAsync(drafter.UserId!, cancellationToken);

                        var newPickDecision = new PickDecisionResponse(
                            pickDecision.Id!.Value.ToString(),
                            new DrafterResponse(
                                drafter.Id!.Value,
                                drafterUser.FirstName,
                                drafterUser.LastName,
                                (bool)drafter.HasRolloverVeto!,
                                (bool)drafter.HasRolloverVetooverride!),
                            new MovieResponse(
                                pickedMovie.Id!.Value,
                                pickedMovie.Title!,
                                pickedMovie.Year!,
                                pickedMovie.ImageUrl!,
                                pickedMovie.ImdbUrl!,
                                movieCrewMembers,
                                movieCastMembers),
                            pickBlessings);

                        newDraftPick.PickDecisions.Add(newPickDecision);
                    }

                    draftPicks.Add(newDraftPick);
                }
            }

            var fullDraft = new DraftResponse(
                draft.Id!.Value.ToString(),
                draft.Name,
                draft.DraftType.ToString(),
                draft.ReleaseDate!.Value.ToString("d"),
                draft.Runtime.ToString()!,
                int.Parse(draft.EpisodeNumber!),
                hosts,
                draftPicks.OrderByDescending(x => x.DraftPosition).ToList());

            result.Add(fullDraft);
        }

        return Result.Success(result);
    }

    private static List<Pick> DuplicatedPicks(List<Pick> picks)
    {
        var orderedListOfPicks = picks
            .OrderBy(o => o.DraftPosition).ToList();

        var picksWhereBlessingsWereUsed = orderedListOfPicks
            .GroupBy(i => i.DraftPosition)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key);

        return picks
            .Where(x => picksWhereBlessingsWereUsed.Contains(x.DraftPosition))
            .ToList();
    }
}
