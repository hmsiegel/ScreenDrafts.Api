namespace ScreenDrafts.Api.Application.Hosts.Queries.GetById;
internal sealed class GetHostByIdQueryHandler : IQueryHandler<GetHostByIdQuery, HostResponse>
{
    private readonly IHostRepository _hostRepository;
    private readonly IUserService _userService;

    public GetHostByIdQueryHandler(IHostRepository hostRepository, IUserService userService)
    {
        _hostRepository = hostRepository;
        _userService = userService;
    }

    public async Task<Result<HostResponse>> Handle(GetHostByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            return Result.Failure<HostResponse>(DomainErrors.User.NotFound);
        }

        var host = await _hostRepository.GetByUserIdAsync(user.Id.ToString(), cancellationToken);

        if (host is null)
        {
            return Result.Failure<HostResponse>(DomainErrors.Host.NotFound);
        }

        var response = new HostResponse(
            host.Id!.Value,
            host.User!.FirstName!,
            host.User.LastName!,
            (int)host.PredictionPoints!);

        return Result.Success(response);
    }
}
