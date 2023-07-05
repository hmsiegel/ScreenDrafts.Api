namespace ScreenDrafts.Api.Application.Common.Exceptions;
public sealed class ForbiddenException : CustomException
{
    public ForbiddenException(
        string message)
        : base(message, null, HttpStatusCode.Forbidden)
    {
    }
}
