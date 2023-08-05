namespace ScreenDrafts.Api.Application.Common.Interfaces.Services;
public interface ICsvFileService
{
    IEnumerable<T> ReadCsvFile<T>(string filePath);
}
