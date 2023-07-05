namespace ScreenDrafts.Api.Infrastructure.Logging;
public sealed class LoggerSettings
{
    public string AppName { get; set; } = "ScreenDrafts.Api";
    public string ElasticSearchUrl { get; set; } = string.Empty;
    public bool WriteToFile { get; set; }
    public bool StructuredConsoleLogging { get; set; }
    public string MinimumLogLevel { get; set; } = "Information";
}
