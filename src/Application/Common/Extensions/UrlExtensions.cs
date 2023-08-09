namespace ScreenDrafts.Api.Application.Common.Extensions;
public static class UrlExtensions
{
    public static string GetLastUrlSegment(this string url)
    {
        Uri uri = new(url);
        return uri.Segments.Last();
    }
}
