namespace ScreenDrafts.Api.Application.Drafts.Commands.UpdateDraft;
public sealed record UpdateDraftCommand(
    DefaultIdType DraftId,
    string Name,
    DraftType DraftType,
    DateTime ReleaseDate,
    int Runtime,
    string EpisodeNumber,
    int NumberOfDrafters,
    int NumberOfFilms) : ICommand;
