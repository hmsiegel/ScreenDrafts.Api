namespace ScreenDrafts.Api.Application.Movies.Commands.AddCrewMember;
public sealed record AddCrewMemberCommand(
    DefaultIdType MovieId,
    DefaultIdType CrewMemberId,
    CrewType CrewType) : ICommand;
