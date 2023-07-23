namespace ScreenDrafts.Api.Application.Hosts.Commands.UpdateHost;
internal sealed class UpdateHostCommandHandler : ICommandHandler<UpdateHostCommand>
{
    private readonly IHostRepository _hostRepository;

    public UpdateHostCommandHandler(IHostRepository hostRepository)
    {
        _hostRepository = hostRepository;
    }

    public async Task<Result> Handle(UpdateHostCommand request, CancellationToken cancellationToken)
    {
        var host = await _hostRepository.GetByHostIdAsync(request.Id, cancellationToken);

        if (host is null)
        {
            return Result.Failure(DomainErrors.Host.NotFound);
        }

        host.AddPredictionPoints(request.PredictionPoints);

        _hostRepository.UpdateHost(host);

        return Result.Success();
    }
}
