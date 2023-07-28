namespace ScreenDrafts.Api.Application.Movies.Queries.GetByMovieId;
public sealed record GetByMovieIdQuery(DefaultIdType Id) : IQuery<MovieResponse>;
