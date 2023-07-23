using ScreenDrafts.Api.Contracts.Drafts.Responses;

namespace ScreenDrafts.Api.Application.Drafts.Queries.GetAll;
public sealed record GetAllDraftsQuery : IQuery<List<DraftResponse>>;
