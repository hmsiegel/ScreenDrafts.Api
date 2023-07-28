namespace ScreenDrafts.Api.Application.Movies.Queries.GetByImdbId;
public sealed record GetMovieByImdbIdQuery(string ImdbId) : IQuery<MovieResponse>;
