namespace ScreenDrafts.Api.Application.Hosts.Queries.GetAll;
internal sealed class GetAllHostsQueryHandler : IQueryHandler<GetAllHostsQuery, List<HostResponse>>
{
    private readonly IHostRepository _hostRepository;

    public GetAllHostsQueryHandler(IHostRepository hostRepository)
    {
        _hostRepository = hostRepository;
    }

    public async Task<Result<List<HostResponse>>> Handle(GetAllHostsQuery request, CancellationToken cancellationToken)
    {
        var hosts = await _hostRepository.GetAllHosts(cancellationToken);

        return hosts.ConvertAll(h => new HostResponse(
            h.Id!.Value,
            h.User!.FirstName!,
            h.User!.LastName!,
            (int)h.PredictionPoints!));
    }
}
