namespace ScreenDrafts.Api.Application.Authentication.Roles;
public sealed class CreateOrUpdateRoleRequestValidator : AbstractValidator<CreateOrUpdateRoleRequest>
{
    public CreateOrUpdateRoleRequestValidator(IRoleService roleService)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MustAsync(async (role, name, _) => !await roleService.ExistsAsync(name, role.Id))
            .WithMessage("Role name already exists.");
    }
}
