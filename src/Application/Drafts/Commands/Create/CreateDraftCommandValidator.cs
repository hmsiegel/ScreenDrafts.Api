namespace ScreenDrafts.Api.Application.Drafts.Commands.Create;
public sealed class CreateDraftCommandValidator : AbstractValidator<CreateDraftCommand>
{
    public CreateDraftCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.DraftType)
            .IsInEnum();

        RuleFor(x => x.NumberOfDrafters)
            .GreaterThan(0);
    }
}
