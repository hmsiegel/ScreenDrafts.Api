namespace ScreenDrafts.Api.Contracts.Drafters;
public sealed record UpdateDrafterRequest(bool HasRolloverVeto, bool HasRolloverVetoOverride);
