namespace ScreenDrafts.Api.Contracts.Hosts;

public sealed record HostResponse(
    DefaultIdType Id,
    string FirstName,
    string LastName,
    int PredictionPoints);