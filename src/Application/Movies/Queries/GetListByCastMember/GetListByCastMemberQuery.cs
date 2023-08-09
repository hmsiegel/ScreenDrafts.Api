namespace ScreenDrafts.Api.Application.Movies.Queries.GetListByCastMember;
public sealed record GetListByCastMemberQuery(string ImdbId) : IQuery<List<MovieResult>>;
