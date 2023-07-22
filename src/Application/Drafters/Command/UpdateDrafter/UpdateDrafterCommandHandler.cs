namespace ScreenDrafts.Api.Application.Drafters.Command.UpdateDrafter;
internal sealed class UpdateDrafterCommandHandler : ICommandHandler<UpdateDrafterCommand>
{
    private readonly IDrafterRepository _drafterRepository;

    public UpdateDrafterCommandHandler(IDrafterRepository drafterRepository)
    {
        _drafterRepository = drafterRepository;
    }

    public async Task<Result> Handle(UpdateDrafterCommand request, CancellationToken cancellationToken)
    {
        var drafter = await _drafterRepository.GetByDrafterIdAsync(request.Id, cancellationToken);

        if (drafter is null)
        {
            return Result.Failure(DomainErrors.Drafter.NotFound);
        }

        if (request.HasRolloverVeto)
        {
            drafter.AddRolloverVeto();
        }
        else
        {
            drafter.RemoveRolloverVeto();
        }

        if (request.HasRolloverVetoOverride)
        {
            drafter.AddRolloverVetooverride();
        }
        else
        {
            drafter.RemoveRolloverVetooverride();
        }

        _drafterRepository.UpdateDrafter(drafter);

        return Result.Success();
    }
}
