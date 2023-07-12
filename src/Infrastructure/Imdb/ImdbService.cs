namespace ScreenDrafts.Api.Infrastructure.Imdb;

public sealed class ImdbService : IImdbService
{
    private readonly ImdbSettings _settings;

    public ImdbService(IOptions<ImdbSettings> settings)
    {
        _settings = settings.Value;
    }

    public ApiLib ApiLib => new(_settings.Key);

    public async Task<SearchData> SearchByKeyword(string searchExpression)
    {
        return await ApiLib.SearchKeywordAsync(searchExpression);
    }

    public async Task<SearchData> SearchByTitle(string searchExpression)
    {
        return await ApiLib.SearchTitleAsync(searchExpression);
    }

    public async Task<SearchData> SearchForMovie(string searchExpression)
    {
        return await ApiLib.SearchMovieAsync(searchExpression);
    }

    public async Task<SearchData> SearchForSeries(string searchExpression)
    {
        return await ApiLib.SearchSeriesAsync(searchExpression);
    }

    public async Task<TitleData> GetMovieInformation(string id, string options)
    {
        return await ApiLib.TitleAsync(id, Language.en, options);
    }

    public async Task<FullCastData> GetFullCast(string id)
    {
        return await ApiLib.FullCastDataAsync(id);
    }
}
