namespace ScreenDrafts.Api.Application.Hosts.Commands.AssignHost;
internal sealed class AssignHostCommandHandler : ICommandHandler<AssignHostCommand, string>
{
    private const string _host = "Commissioner";

    private readonly IHostRepository _hostRepository;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public AssignHostCommandHandler(
        IHostRepository hostRepository,
        IUserService userService,
        IRoleService roleService)
    {
        _hostRepository = hostRepository;
        _userService = userService;
        _roleService = roleService;
    }

    public async Task<Result<string>> Handle(AssignHostCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<string>(DomainErrors.Host.UserNotFound);
        }

        var host = Host.Create(user.Id);

        _hostRepository.Add(host);

        var role = await _roleService.GetByNameAsync(_host, cancellationToken);

        if (role is null)
        {
            return Result.Failure<string>(DomainErrors.Roles.NotFound);
        }

        var roleId = role.Id;

        UserRoleResponse roleResponse = new(roleId,_host,_host, true);

        var roleRequest = new UserRolesRequest();
        roleRequest.UserRoles.Add(roleResponse);

        await _userService.AssignRolesAsync(user.Id, roleRequest, cancellationToken);

        return Result.Success(host.Id!.ToString()!);
    }
}
