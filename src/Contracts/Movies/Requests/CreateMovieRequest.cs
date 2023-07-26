namespace ScreenDrafts.Api.Contracts.Movies.Requests;
public sealed record CreateMovieRequest(
    string Title,
    string Year,
    string ImageUrl,
    string ImdbUrl,
    bool IsInMarqueeOfFame = false);
