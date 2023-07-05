namespace ScreenDrafts.Api.Infrastructure.Common.Services;
public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
