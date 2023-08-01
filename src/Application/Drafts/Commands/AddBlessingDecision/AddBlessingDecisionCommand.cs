namespace ScreenDrafts.Api.Application.Drafts.Commands.AddBlessingDecision;
public sealed record AddBlessingDecisionCommand(
    DefaultIdType DraftId,
    DefaultIdType PickId,
    DefaultIdType MovieId,
    DefaultIdType DrafterId,
    BlessingUsed BlessingUsed) : ICommand;
