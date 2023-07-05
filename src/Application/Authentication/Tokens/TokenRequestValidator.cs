namespace ScreenDrafts.Api.Application.Authentication.Tokens;
public sealed class TokenRequestValidator : AbstractValidator<TokenRequest>
{
    public TokenRequestValidator()
    {
        RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid email address.");

        RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}
