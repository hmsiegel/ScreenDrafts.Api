namespace ScreenDrafts.Api.Application.CrewMembers.Queries.GetAll;
internal sealed class GetAllCrewMembersQueryHandler : IQueryHandler<GetAllCrewMembersQuery, List<CrewMemberResponse>>
{
    private readonly ICrewMemberRepository _crewMemberRepository;

    public GetAllCrewMembersQueryHandler(ICrewMemberRepository crewMemberRepository)
    {
        _crewMemberRepository = crewMemberRepository;
    }

    public async Task<Result<List<CrewMemberResponse>>> Handle(GetAllCrewMembersQuery request, CancellationToken cancellationToken)
    {
        var crewMembers = await _crewMemberRepository.GetAllCrewMembers(cancellationToken);

        return crewMembers.ConvertAll(d => new CrewMemberResponse(
            d.Id!.Value,
            d.ImdbId!,
            d.Name!));
    }
}
