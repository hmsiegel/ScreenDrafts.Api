namespace ScreenDrafts.Api.Application.Drafters.Command.AssignDrafter;
public class AssignDrafterCommandHandler : ICommandHandler<AssignDrafterCommand, string>
{
    private readonly IDrafterRepository _drafterRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public AssignDrafterCommandHandler(
        IDrafterRepository drafterRepository,
        UserManager<ApplicationUser> userManager)
    {
        _drafterRepository = drafterRepository;
        _userManager = userManager;
    }

    public async Task<Result<string>> Handle(AssignDrafterCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user is null)
        {
              return Result.Failure<string>(DomainErrors.Drafter.UserNotFound);
        }

        var drafter = Drafter.Create(user, user.Id);

        _drafterRepository.Add(drafter);
        return Result.Success(drafter.Id);
    }
}
