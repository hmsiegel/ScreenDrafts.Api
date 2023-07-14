namespace ScreenDrafts.Api.Application.Drafters.Queries.GetById;
internal sealed class GetByIdQueryHandler : IQueryHandler<GetByIdQuery, DrafterResponse>
{
    private readonly IDrafterRepository _drafterRepository;

    public GetByIdQueryHandler(IDrafterRepository drafterRepository)
    {
        _drafterRepository = drafterRepository;
    }

    public async Task<Result<DrafterResponse>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var drafter = await _drafterRepository.GetByIdAsync(request.Id, cancellationToken);

        if (drafter is null)
        {
            return Result.Failure<DrafterResponse>(DomainErrors.Drafter.NotFound);
        }

        var response = new DrafterResponse(
            drafter.Id,
            drafter.User!.FirstName!,
            drafter.User.LastName!,
            (bool)drafter.HasRolloverVeto!,
            (bool)drafter.HasRolloverVetooverride!);

        return Result.Success(response);
    }
}
