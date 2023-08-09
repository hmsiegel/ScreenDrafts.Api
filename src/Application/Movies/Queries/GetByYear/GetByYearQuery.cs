namespace ScreenDrafts.Api.Application.Movies.Queries.GetByYear;
public sealed record GetByYearQuery(string Year) : IQuery<List<MovieResult>>;
