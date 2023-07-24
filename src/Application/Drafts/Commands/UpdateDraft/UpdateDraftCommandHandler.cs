namespace ScreenDrafts.Api.Application.Drafts.Commands.UpdateDraft;
internal sealed class UpdateDraftCommandHandler : ICommandHandler<UpdateDraftCommand>
{
    private readonly IDraftRepository _draftRepository;

    public UpdateDraftCommandHandler(IDraftRepository draftRepository)
    {
        _draftRepository = draftRepository;
    }

    public async Task<Result> Handle(UpdateDraftCommand request, CancellationToken cancellationToken)
    {
        var draft = await _draftRepository.GetByIdAsync(request.DraftId, cancellationToken);

        if (draft is null)
        {
            return Result.Failure<Draft>(DomainErrors.Draft.NotFound);
        }

        draft.UpdateName(request.Name);
        draft.UpdateDraftType(request.DraftType);
        draft.UpdateReleaseDate(request.ReleaseDate);
        draft.UpdateRuntime(request.Runtime);
        draft.UpdateEpisodeNumber(request.EpisodeNumber);
        draft.UpdateNumberOfDrafters(request.NumberOfDrafters);
        draft.UpdateNumberOfFilms(request.NumberOfFilms);

        _draftRepository.UpdateDraft(draft);

        return Result.Success("Draft updated successfully.");
    }
}
