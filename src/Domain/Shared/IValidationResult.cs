namespace ScreenDrafts.Api.Domain.Shared;
public interface IValidationResult
{
    public static readonly Error ValidationError = new(
        "Validation Error",
        "A validation problem has occurred.");

    public Error[] Errors { get; }
}
