namespace ScreenDrafts.Api.Application.Common.Exceptions;
public sealed class NotFoundException : CustomException
{
    public NotFoundException(string message)
        : base(message, null, HttpStatusCode.NotFound)
    {
    }
}
