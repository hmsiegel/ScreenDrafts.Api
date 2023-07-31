namespace ScreenDrafts.Api.Application.Drafts.Queries.GetAllDraftersAndHosts;
internal sealed class GetAllDraftsWithDraftersAndHostsQueryHandler
    : IQueryHandler<GetAllDraftsWithDraftersAndHostsQuery, List<DraftWithDraftersAndHostsResponse>>
{
    private readonly IDraftRepository _draftRepository;
    private readonly IDrafterRepository _drafterRepository;
    private readonly IUserService _userService;
    private readonly IHostRepository _hostRepository;

    public GetAllDraftsWithDraftersAndHostsQueryHandler(
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

    public async Task<Result<List<DraftWithDraftersAndHostsResponse>>> Handle(GetAllDraftsWithDraftersAndHostsQuery request, CancellationToken cancellationToken)
    {
        var drafts = await _draftRepository.GetAllDrafts(cancellationToken);
        var drafters = new List<DrafterResponse>();
        var hosts = new List<HostResponse>();

        foreach (var drafterId in drafts.SelectMany(d => d.DrafterIds))
        {
            var drafter = await _drafterRepository.GetByDrafterIdAsync(drafterId.Value, cancellationToken);

            var user = await _userService.GetByIdAsync(drafter!.UserId!, cancellationToken);

            if (drafter is null)
            {
                return Result.Failure<List<DraftWithDraftersAndHostsResponse>>(DomainErrors.Drafter.NotFound);
            }

            drafters.Add(new DrafterResponse(
                drafter.Id!.Value,
                user.FirstName,
                user.LastName,
                (bool)drafter.HasRolloverVeto!,
                (bool)drafter.HasRolloverVetooverride!));
        }

        foreach (var hostId in drafts.SelectMany(d => d.HostIds))
        {
            var host = await _hostRepository.GetByHostIdAsync(hostId.Value, cancellationToken);

            var user = await _userService.GetByIdAsync(host!.UserId!, cancellationToken);

            if (host is null)
            {
                return Result.Failure<List<DraftWithDraftersAndHostsResponse>>(DomainErrors.Drafter.NotFound);
            }

            hosts.Add(new HostResponse(
                host.Id!.Value,
                user.FirstName,
                user.LastName,
                host.PredictionPoints!.Value));
        }

        return drafts.ConvertAll(d => new DraftWithDraftersAndHostsResponse(
            d.Id!.Value,
            d.Name!,
            d.DraftType!.Name!,
            (DateTime)d.ReleaseDate!,
            (int)d.Runtime!,
            d.EpisodeNumber!,
            hosts,
            drafters));
    }
}
