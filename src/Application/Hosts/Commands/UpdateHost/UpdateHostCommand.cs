namespace ScreenDrafts.Api.Application.Hosts.Commands.UpdateHost;
public sealed record UpdateHostCommand(
       DefaultIdType Id,
       int PredictionPoints) : ICommand;
