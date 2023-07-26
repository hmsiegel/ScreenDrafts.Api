namespace ScreenDrafts.Api.Application.CastMembers.Queries.GetAll;
internal sealed class GetAllCastMembersQueryHandler : IQueryHandler<GetAllCastMembersQuery, List<CastMemberResponse>>
{
    private readonly ICastMemberRepository _castMemberRepository;

    public GetAllCastMembersQueryHandler(ICastMemberRepository castMemberRepository)
    {
        _castMemberRepository = castMemberRepository;
    }

    public async Task<Result<List<CastMemberResponse>>> Handle(GetAllCastMembersQuery request, CancellationToken cancellationToken)
    {
        var castMembers = await _castMemberRepository.GetAllCastMembers(cancellationToken);

        return castMembers.ConvertAll(c => new CastMemberResponse(
            c.Id!.Value,
            c.ImdbId!,
            c.Name!));
    }
}
