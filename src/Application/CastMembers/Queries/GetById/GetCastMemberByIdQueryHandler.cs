namespace ScreenDrafts.Api.Application.CastMembers.Queries.GetById;
internal sealed class GetCastMemberByIdQueryHandler : IQueryHandler<GetCastMemberByIdQuery, CastMemberResponse>
{
    private readonly ICastMemberRepository _castMemberRepository;

    public GetCastMemberByIdQueryHandler(ICastMemberRepository castMemberRepository)
    {
        _castMemberRepository = castMemberRepository;
    }

    public async Task<Result<CastMemberResponse>> Handle(GetCastMemberByIdQuery request, CancellationToken cancellationToken)
    {
        var castMember = await _castMemberRepository.GetByCastMemberIdAsync(request.Id, cancellationToken);

        if (castMember is null)
        {
            return Result.Failure<CastMemberResponse>(DomainErrors.CastMember.NotFound);
        }

        var response = new CastMemberResponse(
            castMember.Id!.Value,
            castMember.ImdbId!,
            castMember.Name!);

        return Result.Success(response);
    }
}
