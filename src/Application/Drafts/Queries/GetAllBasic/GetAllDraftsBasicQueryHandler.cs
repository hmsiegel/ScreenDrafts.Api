namespace ScreenDrafts.Api.Application.Drafts.Queries.GetAll;
internal sealed class GetAllDraftsBasicQueryHandler : IQueryHandler<GetAllDraftsBasicQuery, List<BasicDraftResponse>>
{
    private readonly IDraftRepository _draftRepository;

    public GetAllDraftsBasicQueryHandler(IDraftRepository draftRepository)
    {
        _draftRepository = draftRepository;
    }

    public async Task<Result<List<BasicDraftResponse>>> Handle(GetAllDraftsBasicQuery request, CancellationToken cancellationToken)
    {
        var drafts = await _draftRepository.GetAllDrafts(cancellationToken);

        return drafts.ConvertAll(d => new BasicDraftResponse(
            d.Id!.Value,
            d.Name!,
            d.DraftType!.Name!,
            (DateTime)d.ReleaseDate!,
            (int)d.Runtime!,
            d.EpisodeNumber!));
    }
}
