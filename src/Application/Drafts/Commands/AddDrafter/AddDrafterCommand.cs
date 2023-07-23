namespace ScreenDrafts.Api.Application.Drafts.Commands.AddDrafter;
public sealed record AddDrafterCommand(DefaultIdType DraftId, string UserId) : ICommand;
