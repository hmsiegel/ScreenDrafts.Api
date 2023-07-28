namespace ScreenDrafts.Api.Application.Movies.Commands.AddCastMember;
public sealed record AddCastMemberCommand(
    DefaultIdType MovieId,
    DefaultIdType CastMemberId,
    string RoleDescription) : ICommand;
