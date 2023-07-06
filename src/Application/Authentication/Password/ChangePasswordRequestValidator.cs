namespace ScreenDrafts.Api.Application.Authentication.Password;
public sealed class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty();

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .NotEqual(x => x.Password);

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .Equal(x => x.NewPassword)
        .WithMessage("Passwords do not match.");
    }
}
