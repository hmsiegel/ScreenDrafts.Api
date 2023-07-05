namespace ScreenDrafts.Api.Infrastructure.FileStorage;
public sealed partial class LocalFileStorageService : IFileStorageService
{
    private const string _numberPattern = "-{0}";

    public static string RemoveSpecialCharacters(string str)
    {
        return AlphaNumericRegex().Replace(str, string.Empty);
    }

    public async Task<string> UploadAsync<T>(FileUploadRequest? request, FileType supportedFileType, CancellationToken cancellationToken = default)
        where T : class
    {
        if (request is null && request!.Data is null)
        {
            return string.Empty;
        }

        if (request.Extension is null && !supportedFileType.GetDescriptionList().Contains(request.Extension!.ToLower()))
        {
            throw new InvalidOperationException($"File type {request.Extension} is not supported.");
        }

        if (request.Name is null)
        {
            throw new InvalidOperationException("File name is required.");
        }

        string base64Data = FileImageRegex().Match(request.Data).Groups["data"].Value;

        var streamData = new MemoryStream(Convert.FromBase64String(base64Data));

        if (streamData.Length > 0)
        {
            string folder = typeof(T).Name;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                folder = folder.Replace(@"\", "/", StringComparison.InvariantCultureIgnoreCase);
            }

            string folderName = supportedFileType switch
            {
                FileType.Image => Path.Combine("Files", "Images", folder),
                _ => Path.Combine("Files", "Others", folder),
            };
            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            Directory.CreateDirectory(pathToSave);

            string fileName = request.Name.Trim('"');
            fileName = RemoveSpecialCharacters(fileName);
            fileName = fileName.ReplaceWhitespace("-");
            fileName += request.Extension.Trim();
            string fullPath = Path.Combine(pathToSave, fileName);
            string dbPath = Path.Combine(folderName, fileName);
            if (File.Exists(dbPath))
            {
                dbPath = NextAvailableFilename(dbPath);
                fullPath = NextAvailableFilename(fullPath);
            }

            using var stream = new FileStream(fullPath, FileMode.Create);
            await streamData.CopyToAsync(stream, cancellationToken);
            return dbPath.Replace("\\", "/", StringComparison.InvariantCultureIgnoreCase);
        }
        else
        {
            return string.Empty;
        }
    }

    public void Remove(string? path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    private static string NextAvailableFilename(string path)
    {
        if (!File.Exists(path))
        {
            return path;
        }

        if (Path.HasExtension(path))
        {
            return GetNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path), StringComparison.Ordinal), _numberPattern));
        }

        return GetNextFilename(path + _numberPattern);
    }

    private static string GetNextFilename(string pattern)
    {
        string tmp = string.Format(pattern, 1);

        if (!File.Exists(tmp))
        {
            return tmp;
        }

        int min = 1, max = 2;

        while (File.Exists(string.Format(pattern, max)))
        {
            min = max;
            max *= 2;
        }

        while (max != min + 1)
        {
            int pivot = (max + min) / 2;
            if (File.Exists(string.Format(pattern, pivot)))
            {
                min = pivot;
            }
            else
            {
                max = pivot;
            }
        }

        return string.Format(pattern, max);
    }

    [GeneratedRegex("data:image/(?<type>.+?),(?<data>.+)")]
    private static partial Regex FileImageRegex();
    [GeneratedRegex("[^a-zA-Z0-9_.]+", RegexOptions.Compiled)]
    private static partial Regex AlphaNumericRegex();
}