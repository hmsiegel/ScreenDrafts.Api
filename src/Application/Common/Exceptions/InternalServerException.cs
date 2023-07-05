namespace ScreenDrafts.Api.Application.Common.Exceptions;
public sealed class InternalServerException : CustomException
{
    public InternalServerException(
        string message,
        List<string>? errors = default)
        : base(message, errors, HttpStatusCode.InternalServerError)
    {
    }
}
