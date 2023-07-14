using ScreenDrafts.Api.Domain.DraftAggregate.Enums;

namespace ScreenDrafts.Api.Application.Drafts.Commands.Create;
public sealed record CreateDraftCommand(
    string Name,
    DraftType DraftType,
    int NumberOfDrafters) : ICommand<string>;
