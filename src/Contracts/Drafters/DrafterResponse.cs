namespace ScreenDrafts.Api.Contracts.Drafters;
public sealed record DrafterResponse(
    DefaultIdType Id,
    string FirstName,
    string LastName,
    bool HasRolloverVeto,
    bool HasRolloverVetoOverride);
