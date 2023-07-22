namespace ScreenDrafts.Api.Application.Drafters.Queries.GetAll;
internal sealed class GetAllQueryHandler : IQueryHandler<GetAllQuery, List<DrafterResponse>>
{
    private readonly IDrafterRepository _drafterRepository;

    public GetAllQueryHandler(IDrafterRepository drafterRepository)
    {
        _drafterRepository = drafterRepository;
    }

    public async Task<Result<List<DrafterResponse>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
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
