namespace ScreenDrafts.Api.Application.CastMembers.Commands.Create;
public sealed record CreateCastMemberCommand(string ImdbId, string Name) : ICommand<CastMemberId>;
