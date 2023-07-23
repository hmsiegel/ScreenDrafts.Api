namespace ScreenDrafts.Api.Application.Drafts.Commands.AddHost;
internal sealed class AddHostCommandHandler : ICommandHandler<AddHostCommand>
{
    private readonly IDraftRepository _draftRepository;
    private readonly IHostRepository _hostRepository;
    private readonly IUserService _userService;

    public AddHostCommandHandler(
        IDraftRepository draftRepository,
        IHostRepository hostRepository,
        IUserService userService)
    {
        _draftRepository = draftRepository;
        _hostRepository = hostRepository;
        _userService = userService;
    }

    public async Task<Result> Handle(AddHostCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(request.UserId, cancellationToken);
        var draft = await _draftRepository.GetByIdAsync(request.DraftId, cancellationToken);

        if (draft is null)
        {
            return Result.Failure<Draft>(DomainErrors.Draft.NotFound);
        }

        var host = await _hostRepository.GetByUserIdAsync(user.Id.ToString(), cancellationToken);

        if (host is null)
        {
            return Result.Failure<Draft>(DomainErrors.Host.NotFound);
        }

        await _draftRepository.AddHostAsync(draft, host);

        return Result.Success();
    }
}
