namespace ScreenDrafts.Api.Contracts.FileStorage;
public sealed record FileUploadRequest(
    string Name,
    string Extension,
    string Data);
