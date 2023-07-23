namespace ScreenDrafts.Api.Application.Drafters.Queries.GetById;
public sealed record GetDrafterByIdQuery(string Id) : IQuery<DrafterResponse>;
