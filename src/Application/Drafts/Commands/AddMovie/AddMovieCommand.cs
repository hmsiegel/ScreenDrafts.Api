namespace ScreenDrafts.Api.Application.Drafts.Commands.AddMovie;
public sealed record AddMovieCommand(
    DefaultIdType DraftId,
    DefaultIdType MovieId,
    int DraftPosition,
    PickDecisionCommand PickDecision) : ICommand;

public  sealed record PickDecisionCommand(
    DefaultIdType UserId,
    Decision Decision);