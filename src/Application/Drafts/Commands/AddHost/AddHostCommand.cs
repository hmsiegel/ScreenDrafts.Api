namespace ScreenDrafts.Api.Application.Drafts.Commands.AddHost;
public sealed record AddHostCommand(DefaultIdType DraftId, string UserId) : ICommand;
