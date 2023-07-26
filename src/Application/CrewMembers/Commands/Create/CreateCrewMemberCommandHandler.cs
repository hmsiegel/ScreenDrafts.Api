namespace ScreenDrafts.Api.Application.CrewMembers.Commands.Create;
internal sealed class CreateCrewMemberCommandHandler : ICommandHandler<CreateCrewMemberCommand, CrewMemberId>
{
    private readonly ICrewMemberRepository _crewMemberRepository;

    public CreateCrewMemberCommandHandler(ICrewMemberRepository crewMemberRepository)
    {
        _crewMemberRepository = crewMemberRepository;
    }

    public Task<Result<CrewMemberId>> Handle(CreateCrewMemberCommand request, CancellationToken cancellationToken)
    {
        var crewMember = CrewMember.Create(request.ImdbId, request.Name);

        _crewMemberRepository.Add(crewMember);

        return Task.FromResult(Result<CrewMemberId>.Success(crewMember.Id))!;
    }
}
