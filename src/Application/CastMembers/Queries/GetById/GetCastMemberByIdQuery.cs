namespace ScreenDrafts.Api.Application.CastMembers.Queries.GetById;
public sealed record GetCastMemberByIdQuery(DefaultIdType Id) : IQuery<CastMemberResponse>;
