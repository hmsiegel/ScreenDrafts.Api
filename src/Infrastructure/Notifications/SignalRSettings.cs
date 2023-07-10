namespace ScreenDrafts.Api.Infrastructure.Notifications;
public sealed class SignalRSettings
{
    public bool UseBackplane { get; set; }

    public class Backplane
    {
        public string? Provider { get; set; }
        public string? ConnectionString { get; set; }
    }
}
