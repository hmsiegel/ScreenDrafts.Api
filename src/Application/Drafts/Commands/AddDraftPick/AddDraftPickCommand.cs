namespace ScreenDrafts.Api.Application.Drafts.Commands.AddMovie;
public sealed record AddDraftPickCommand(
    DefaultIdType DraftId,
    int DraftPosition,
    DefaultIdType DrafterId,
    DefaultIdType MovieId) : ICommand;
