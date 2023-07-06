namespace ScreenDrafts.Api.Application.Common.Exceptions;
public sealed class ConflictException : CustomException
{
    public ConflictException(
        string message)
        : base(message, null, HttpStatusCode.Conflict)
    {
    }
}
