namespace ScreenDrafts.Api.Application.CrewMembers.Queries.GetById;
public sealed record GetCrewMemberByIdQuery(DefaultIdType Id) : IQuery<CrewMemberResponse>;
