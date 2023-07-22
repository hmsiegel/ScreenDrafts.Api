using ScreenDrafts.Api.Application.Authentication.Roles;

namespace ScreenDrafts.Api.Application.Drafters.Command.AssignDrafter;
public class AssignDrafterCommandHandler : ICommandHandler<AssignDrafterCommand, string>
{
    private const string _drafter = "Drafter";

    private readonly IDrafterRepository _drafterRepository;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public AssignDrafterCommandHandler(
        IDrafterRepository drafterRepository,
        IUserService userService,
        IRoleService roleService)
    {
        _drafterRepository = drafterRepository;
        _userService = userService;
        _roleService = roleService;
    }

    public async Task<Result<string>> Handle(AssignDrafterCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<string>(DomainErrors.Drafter.UserNotFound);
        }

        var drafter = Drafter.Create(user.Id);

        _drafterRepository.Add(drafter);

        var role = await _roleService.GetByNameAsync(_drafter, cancellationToken);

        if (role is null)
        {
            return Result.Failure<string>(DomainErrors.Roles.NotFound);
        }

        var roleId = role.Id;

        UserRoleResponse roleResponse = new(roleId,_drafter,_drafter, true);

        var roleRequest = new UserRolesRequest();
        roleRequest.UserRoles.Add(roleResponse);

        await _userService.AssignRolesAsync(user.Id, roleRequest, cancellationToken);

        return Result.Success(drafter.Id!.ToString()!);
    }
}
