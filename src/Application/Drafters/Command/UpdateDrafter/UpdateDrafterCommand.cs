namespace ScreenDrafts.Api.Application.Drafters.Command.UpdateDrafter;
public sealed record UpdateDrafterCommand(DefaultIdType Id, bool HasRolloverVeto, bool HasRolloverVetoOverride) : ICommand;
