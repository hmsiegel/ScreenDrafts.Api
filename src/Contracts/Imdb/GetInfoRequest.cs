namespace ScreenDrafts.Api.Contracts.Imdb;
public sealed class GetInfoRequest
{
    public string Id { get; set; } = string.Empty;
    public string? Options { get; set; } = string.Empty;
}
