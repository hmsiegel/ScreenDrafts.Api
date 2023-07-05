namespace ScreenDrafts.Api.Domain.Shared;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
public sealed class ValidationResult : Result, IValidationResult
{
    private ValidationResult(Error[] errors)
        : base(false, IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public Error[] Errors { get; }

    public static ValidationResult WithErrors(Error[] errors) =>
        new(errors);
}
