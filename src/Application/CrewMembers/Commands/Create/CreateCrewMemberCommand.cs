namespace ScreenDrafts.Api.Application.CrewMembers.Commands.Create;
public sealed record CreateCrewMemberCommand(string ImdbId, string Name) : ICommand<CrewMemberId>;
