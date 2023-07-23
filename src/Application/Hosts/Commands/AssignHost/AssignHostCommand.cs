namespace ScreenDrafts.Api.Application.Hosts.Commands.AssignHost;
public sealed record AssignHostCommand(string UserId) : ICommand<string>;
