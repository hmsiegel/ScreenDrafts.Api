using System.Runtime.CompilerServices;

namespace ScreenDrafts.Api.Persistence;
internal static class ProjectSourcePath
{
    private const string _myRelativePath = nameof(ProjectSourcePath) + ".cs";
    private static string? _lazyValue;
    public static string Value => _lazyValue ??= CalculatePath();

    public static string GetSourceFilePathName([CallerFilePath] string? callerFilePath = null) =>
        callerFilePath ?? throw new ArgumentNullException(nameof(callerFilePath));

    private static string CalculatePath()
    {
        string pathName = GetSourceFilePathName();
        return pathName[..^_myRelativePath.Length];
    }
}