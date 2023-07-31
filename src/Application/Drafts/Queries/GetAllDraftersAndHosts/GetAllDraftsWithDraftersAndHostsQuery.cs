namespace ScreenDrafts.Api.Application.Drafts.Queries.GetAllDraftersAndHosts;
public sealed record GetAllDraftsWithDraftersAndHostsQuery() 
    : IQuery<List<DraftWithDraftersAndHostsResponse>>;
