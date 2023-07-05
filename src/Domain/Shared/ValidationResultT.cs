namespace ScreenDrafts.Api.Domain.Shared;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    private ValidationResult(Error[] errors)
        : base(default, false, IValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public Error[] Errors { get; }

    public static ValidationResult<TValue> WithErrors(Error[] errors) =>
        new(errors);
}
