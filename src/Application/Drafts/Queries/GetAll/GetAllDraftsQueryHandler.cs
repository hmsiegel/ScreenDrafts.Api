using ScreenDrafts.Api.Contracts.Drafts.Responses;

namespace ScreenDrafts.Api.Application.Drafts.Queries.GetAll;
internal sealed class GetAllDraftsQueryHandler : IQueryHandler<GetAllDraftsQuery, List<DraftResponse>>
{
    private readonly IDraftRepository _draftRepository;
    private readonly IDrafterRepository _drafterRepository;

    public GetAllDraftsQueryHandler(IDraftRepository draftRepository, IDrafterRepository drafterRepository)
    {
        _draftRepository = draftRepository;
        _drafterRepository = drafterRepository;
    }

    public async Task<Result<List<DraftResponse>>> Handle(GetAllDraftsQuery request, CancellationToken cancellationToken)
    {
        var drafts = await _draftRepository.GetAllDrafts(cancellationToken);
        var drafters = new List<DrafterResponse>();

        foreach (var drafterId in drafts.SelectMany(d => d.DrafterIds))
        {
            var drafter = await _drafterRepository.GetByDrafterIdAsync(drafterId.Value, cancellationToken);

            if (drafter is null)
            {
                return Result.Failure<List<DraftResponse>>(DomainErrors.Drafter.NotFound);
            }

            drafters.Add(new DrafterResponse(
                drafter.Id!.Value,
                drafter.User!.FirstName!,
                drafter.User.LastName!,
                (bool)drafter.HasRolloverVeto!,
                (bool)drafter.HasRolloverVetooverride!));
        }

        return drafts.ConvertAll(d => new DraftResponse(
            d.Id!.Value,
            d.Name!,
            d.DraftType!.Name!,
            (DateTime)d.ReleaseDate!,
            (int)d.Runtime!,
            d.EpisodeNumber!,
            drafters));
    }
}
