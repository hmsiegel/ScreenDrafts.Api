namespace ScreenDrafts.Api.Application.Hosts.Queries.GetById;
public sealed record GetHostByIdQuery(string Id) : IQuery<HostResponse>;
