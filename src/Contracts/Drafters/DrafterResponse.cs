namespace ScreenDrafts.Api.Contracts.Drafters;
public sealed record DrafterResponse(
    string Id,
    string FirstName,
    string LastName,
    bool HasRolloverVeto,
    bool HasRolloverVetoOverride);
