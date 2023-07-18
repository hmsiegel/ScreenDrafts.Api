namespace ScreenDrafts.Api.Application.Drafts.Commands.Create;
internal sealed class CreateDraftCommandHandler : ICommandHandler<CreateDraftCommand, string>
{
    private readonly IDraftRepository _draftRepository;

    public CreateDraftCommandHandler(IDraftRepository draftRepository)
    {
        _draftRepository = draftRepository;
    }

    public Task<Result<string>> Handle(CreateDraftCommand request, CancellationToken cancellationToken)
    {
        _draftRepository.Add(Draft.Create(
            request.Name,
            request.DraftType,
            request.NumberOfDrafters));

        return Task.FromResult(Result.Success("Draft created successfully."));
    }
}
