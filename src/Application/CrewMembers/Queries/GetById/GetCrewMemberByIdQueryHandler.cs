namespace ScreenDrafts.Api.Application.CrewMembers.Queries.GetById;
internal sealed class GetCrewMemberByIdQueryHandler : IQueryHandler<GetCrewMemberByIdQuery, CrewMemberResponse>
{
    private readonly ICrewMemberRepository _crewMemberRepository;

    public GetCrewMemberByIdQueryHandler(ICrewMemberRepository crewMemberRepository)
    {
        _crewMemberRepository = crewMemberRepository;
    }

    public async Task<Result<CrewMemberResponse>> Handle(GetCrewMemberByIdQuery request, CancellationToken cancellationToken)
    {
        var crewMember = await _crewMemberRepository.GetByCrewMemberIdAsync(request.Id, cancellationToken);

        if (crewMember is null)
        {
            return Result.Failure<CrewMemberResponse>(DomainErrors.CrewMember.NotFound);
        }

        var response = new CrewMemberResponse(
            crewMember.Id!.Value,
            crewMember.ImdbId!,
            crewMember.Name!);

        return Result.Success(response);
    }
}
