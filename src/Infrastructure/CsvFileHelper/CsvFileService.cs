namespace ScreenDrafts.Api.Infrastructure.CsvFileHelper;
public sealed class CsvFileService : ICsvFileService
{
    public IEnumerable<T> ReadCsvFile<T>(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<T>();
        return records.ToList();
    }
}
