namespace ScreenDrafts.Api.Application.Drafters.Queries.GetAll;
internal sealed class GetAllDraftersQueryHandler : IQueryHandler<GetAllDraftersQuery, List<DrafterResponse>>
{
    private readonly IDrafterRepository _drafterRepository;

    public GetAllDraftersQueryHandler(IDrafterRepository drafterRepository)
    {
        _drafterRepository = drafterRepository;
    }

    public async Task<Result<List<DrafterResponse>>> Handle(GetAllDraftersQuery request, CancellationToken cancellationToken)
    {
        var drafters = await _drafterRepository.GetAllDrafters(cancellationToken);

        return drafters.ConvertAll(d => new DrafterResponse(
            d.Id!.Value,
            d.User!.FirstName!,
            d.User!.LastName!,
            (bool)d.HasRolloverVeto!,
            (bool)d.HasRolloverVetooverride!));
    }
}
