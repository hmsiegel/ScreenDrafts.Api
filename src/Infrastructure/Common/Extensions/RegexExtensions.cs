namespace ScreenDrafts.Api.Infrastructure.Common.Extensions;
public static partial class RegexExtensions
{
    private static readonly Regex _whitespace = WhitespaceRegex();

    public static string ReplaceWhitespace(this string input, string replacement)
    {
        return _whitespace.Replace(input, replacement);
    }

    [GeneratedRegex("\\s+")]
    private static partial Regex WhitespaceRegex();
}
