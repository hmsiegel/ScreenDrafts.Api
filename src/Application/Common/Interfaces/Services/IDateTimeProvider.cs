namespace ScreenDrafts.Api.Application.Common.Interfaces.Services;
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
