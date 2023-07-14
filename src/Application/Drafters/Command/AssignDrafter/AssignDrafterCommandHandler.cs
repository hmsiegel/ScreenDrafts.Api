namespace ScreenDrafts.Api.Application.Drafters.Command.AssignDrafter;
public class AssignDrafterCommandHandler : ICommandHandler<AssignDrafterCommand, string>
{
    private readonly IDrafterRepository _drafterRepository;
    private readonly IUserService _userService;

    public AssignDrafterCommandHandler(
        IDrafterRepository drafterRepository,
        IUserService userService)
    {
        _drafterRepository = drafterRepository;
        _userService = userService;
    }

    public async Task<Result<string>> Handle(AssignDrafterCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetAsync(request.UserId, cancellationToken);

        if (user is null)
        {
              return Result.Failure<string>(DomainErrors.Drafter.UserNotFound);
        }

        var drafter = Drafter.Create(user, user.Id);

        _drafterRepository.Add(drafter);
        return Result.Success(drafter.Id);
    }
}
