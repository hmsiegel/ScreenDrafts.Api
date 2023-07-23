namespace ScreenDrafts.Api.Application.Drafters.Queries.GetById;
internal sealed class GetDrafterByIdQueryHandler : IQueryHandler<GetDrafterByIdQuery, DrafterResponse>
{
    private readonly IDrafterRepository _drafterRepository;
    private readonly IUserService _userService;

    public GetDrafterByIdQueryHandler(IDrafterRepository drafterRepository, IUserService userService)
    {
        _drafterRepository = drafterRepository;
        _userService = userService;
    }

    public async Task<Result<DrafterResponse>> Handle(GetDrafterByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            return Result.Failure<DrafterResponse>(DomainErrors.User.NotFound);
        }

        var drafter = await _drafterRepository.GetByUserIdAsync(user.Id.ToString(), cancellationToken);

        if (drafter is null)
        {
            return Result.Failure<DrafterResponse>(DomainErrors.Drafter.NotFound);
        }

        var response = new DrafterResponse(
            drafter.Id!.Value,
            drafter.User!.FirstName!,
            drafter.User.LastName!,
            (bool)drafter.HasRolloverVeto!,
            (bool)drafter.HasRolloverVetooverride!);

        return Result.Success(response);
    }
}
