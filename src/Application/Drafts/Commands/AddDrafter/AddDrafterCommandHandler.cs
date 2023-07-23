namespace ScreenDrafts.Api.Application.Drafts.Commands.AddDrafter;
internal sealed class AddDrafterCommandHandler : ICommandHandler<AddDrafterCommand>
{
    private readonly IDraftRepository _draftRepository;
    private readonly IDrafterRepository _drafterRepository;
    private readonly IUserService _userService;

    public AddDrafterCommandHandler(
        IDraftRepository draftRepository,
        IDrafterRepository drafterRepository,
        IUserService userService)
    {
        _draftRepository = draftRepository;
        _drafterRepository = drafterRepository;
        _userService = userService;
    }

    public async Task<Result> Handle(AddDrafterCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(request.UserId,cancellationToken);
        var draft = await _draftRepository.GetByIdAsync(request.DraftId, cancellationToken);

        if (draft is null)
        {
            return Result.Failure<Draft>(DomainErrors.Draft.NotFound);
        }

        var drafter = await _drafterRepository.GetByUserIdAsync(user.Id.ToString(), cancellationToken);

        if (drafter is null)
        {
            return Result.Failure<Draft>(DomainErrors.Drafter.NotFound);
        }

        await _draftRepository.AddDrafterAsync(draft, drafter);

        return Result.Success();
    }
}
