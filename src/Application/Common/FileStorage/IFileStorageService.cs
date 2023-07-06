using ScreenDrafts.Api.Application.Common.Interfaces.Services;

namespace ScreenDrafts.Api.Application.Common.FileStorage;
public interface IFileStorageService : ITransientService
{
    public Task<string> UploadAsync<T>(FileUploadRequest? request, FileType supportedFileType, CancellationToken cancellationToken = default)
        where T : class;

    public void Remove(string? path);
}
