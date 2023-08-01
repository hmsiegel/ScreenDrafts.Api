namespace ScreenDrafts.Api.Application.Drafts.Commands.AddBlessingDecision;
internal sealed class AddBlessingDecisionCommandHandler : ICommandHandler<AddBlessingDecisionCommand>
{
    private readonly IDraftRepository _draftRepository;

    public AddBlessingDecisionCommandHandler(IDraftRepository draftRepository)
    {
        _draftRepository = draftRepository;
    }

    public async Task<Result> Handle(AddBlessingDecisionCommand request, CancellationToken cancellationToken)
    {
        var draft = await _draftRepository.GetByIdAsync(request.DraftId, cancellationToken);

        if (draft is null)
        {
            return Result.Failure<Draft>(DomainErrors.Draft.NotFound);
        }

        var pick = draft.Picks!.FirstOrDefault(p => p.Id!.Value == request.PickId);

        if (pick is null)
        {
            return Result.Failure<Draft>(DomainErrors.Pick.NotFound);
        }

        var drafterId = draft.DrafterIds!.FirstOrDefault(d => d.Value == request.DrafterId);

        if (drafterId is null)
        {
            return Result.Failure<Draft>(DomainErrors.Drafter.NotFound);
        }

        var pickDecision = pick.PickDecisions
            .FirstOrDefault(p => p.MovieId!.Value == request.MovieId);

        var blessingDecision = BlessingDecision.Create(
            drafterId,
            request.BlessingUsed);

        pickDecision!.AddBlessingDecision(blessingDecision);

        draft.UpdatePick(pick, pickDecision, blessingDecision);

        return Result.Success();
    }
}
