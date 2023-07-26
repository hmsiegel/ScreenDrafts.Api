namespace ScreenDrafts.Api.Application.CastMembers.Commands.Create;
internal sealed class CreateCastMemberCommandHandler : ICommandHandler<CreateCastMemberCommand, CastMemberId>
{
    private readonly ICastMemberRepository _castMemberRepository;

    public CreateCastMemberCommandHandler(ICastMemberRepository castMemberRepository)
    {
        _castMemberRepository = castMemberRepository;
    }

    public Task<Result<CastMemberId>> Handle(CreateCastMemberCommand request, CancellationToken cancellationToken)
    {
        var castMember = CastMember.Create(request.ImdbId, request.Name);

        _castMemberRepository.Add(castMember);

        return Task.FromResult(Result.Success(castMember.Id))!;
    }
}
