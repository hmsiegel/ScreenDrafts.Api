namespace ScreenDrafts.Api.Presentation.Controllers;
public class ImdbController : VersionedApiController
{
    private readonly IImdbService _imdbService;

    public ImdbController(IImdbService imdbService)
    {
        _imdbService = imdbService;
    }

    [HttpGet("title")]
    [HasPermission(ScreenDraftsAction.Search, ScreenDraftsResource.Imdb)]
    [OpenApiOperation("Search", "Search for a movie or series by title.")]
    public async Task<IActionResult> SearchByTitle(string title)
    {
        var result = await _imdbService.SearchByTitle(title);
        return Ok(result);
    }

    [HttpGet("keyword")]
    [HasPermission(ScreenDraftsAction.Search, ScreenDraftsResource.Imdb)]
    [OpenApiOperation("Search", "Search for a movie or series by keyword.")]
    public async Task<IActionResult> SearchByKeyword(string keyword)
    {
        var result = await _imdbService.SearchByKeyword(keyword);
        return Ok(result);
    }

    [HttpGet("movieinfo")]
    [HasPermission(ScreenDraftsAction.Search, ScreenDraftsResource.Imdb)]
    [OpenApiOperation("Search", "Get Movie Information")]
    public async Task<IActionResult> GetTitleInfo([FromQuery] GetInfoRequest request)
    {
        TitleData? result;
        if (request.Options is null)
        {
            request.Options = string.Empty;
            result = await _imdbService.GetMovieInformation(request.Id);
        }
        else
        {
            result = await _imdbService.GetMovieInformation(
                request.Id,
                Enum.Parse<TitleOptions>(request.Options));
        }

        return Ok(result);
    }
}
