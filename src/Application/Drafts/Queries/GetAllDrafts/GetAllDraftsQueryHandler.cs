namespace ScreenDrafts.Api.Application.Drafts.Queries.GetAllDrafts;
internal sealed class GetAllDraftsQueryHandler : IQueryHandler<GetAllDraftsQuery, List<FullDraftResponse>>
{
    private readonly IDraftRepository _draftRepository;
    private readonly IDrafterRepository _drafterRepository;
    private readonly IUserService _userService;
    private readonly IHostRepository _hostRepository;
    private readonly IMovieRepository _movieRepository;
    private readonly ICrewMemberRepository _crewMemberRepository;
    private readonly ICastMemberRepository _castMemberRepository;

    public GetAllDraftsQueryHandler(
        IDraftRepository draftRepository,
        IDrafterRepository drafterRepository,
        IUserService userService,
        IHostRepository hostRepository,
        IMovieRepository movieRepository,
        ICrewMemberRepository crewMemberRepository,
        ICastMemberRepository castMemberRepository)
    {
        _draftRepository = draftRepository;
        _drafterRepository = drafterRepository;
        _userService = userService;
        _hostRepository = hostRepository;
        _movieRepository = movieRepository;
        _crewMemberRepository = crewMemberRepository;
        _castMemberRepository = castMemberRepository;
    }

    public async Task<Result<List<FullDraftResponse>>> Handle(GetAllDraftsQuery request, CancellationToken cancellationToken)
    {
        var result = new List<FullDraftResponse>();
        var drafts = await _draftRepository.GetAllDrafts(cancellationToken);

        foreach (var draft in drafts)
        {
            var drafters = new List<DrafterResponse>();
            var hosts = new List<HostResponse>();
            var draftPicks = new List<PickResponse>();
            var pickDecisions = new List<PickDecisionResponse>();
            var blessingDecisions = new List<BlessingDecisionResponse>();

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

            foreach (var draftPick in draft.Picks!)
            {
                foreach (var pickDecision in draftPick.PickDecisions)
                {
                    foreach (var blessingDecision in pickDecision.BlessingDecisions!)
                    {
                        var blessingDrafter = await _drafterRepository.GetByDrafterIdAsync(blessingDecision.DrafterId!.Value, cancellationToken);

                        var blessingUser = await _userService.GetByIdAsync(blessingDrafter!.UserId!, cancellationToken);

                        blessingDecisions.Add(new BlessingDecisionResponse(
                            new DrafterResponse(
                                blessingDrafter.Id!.Value,
                                blessingUser.FirstName,
                                blessingUser.LastName,
                                (bool)blessingDrafter.HasRolloverVeto!,
                                (bool)blessingDrafter.HasRolloverVetooverride!),
                            blessingDecision.BlessingUsed!.Name));
                    }

                    var pickedMovie = await _movieRepository.GetByIdAsync(pickDecision.MovieId!.Value);

                    List<MovieCrewMemberResponse> movieCrewMembers = GetCrewMembers(pickedMovie);

                    List<MovieCastMemberResponse> movieCastMembers = GetCastMembers(pickedMovie);

                    pickDecisions.Add(new PickDecisionResponse(
                        pickDecision.Id!.Value,
                        new MovieResponse(
                            pickedMovie.Id!.Value,
                            pickedMovie.Title!,
                            pickedMovie.Year!,
                            pickedMovie.ImageUrl!,
                            pickedMovie.ImdbUrl!,
                            movieCrewMembers,
                            movieCastMembers),
                        blessingDecisions));
                }

                draftPicks.Add(new PickResponse(
                    draftPick.Id!.Value,
                    draftPick.DraftPosition!,
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
                draftPicks);

            result.Add(fullDraft);
        }

        return Result.Success(result);
    }

    private List<MovieCastMemberResponse> GetCastMembers(Movie? movie)
    {
        return _movieRepository.GetAllMovieCastMembers(movie!.Id!.Value)
                        .Result
                        .ConvertAll(x => new MovieCastMemberResponse(
                            x.CastMemberId.Value,
                            _castMemberRepository.GetByCastMemberIdAsync(x.CastMemberId.Value).Result.Name!,
                            _castMemberRepository.GetByCastMemberIdAsync(x.CastMemberId.Value).Result.ImdbId!,
                            x.RoleDescription!));
    }

    private List<MovieCrewMemberResponse> GetCrewMembers(Movie? movie)
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
