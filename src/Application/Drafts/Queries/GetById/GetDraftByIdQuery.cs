namespace ScreenDrafts.Api.Application.Drafts.Queries.GetById;
public sealed record GetDraftByIdQuery(string Id) : IQuery<Draft>;
