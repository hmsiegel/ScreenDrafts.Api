namespace ScreenDrafts.Api.Application.Drafters.Queries.GetById;
public sealed record GetByIdQuery(string Id) : IQuery<DrafterResponse>;
