namespace ScreenDrafts.Api.Application.Drafters.Command.AssignDrafter;
public sealed record AssignDrafterCommand(string UserId) : ICommand<string>;
