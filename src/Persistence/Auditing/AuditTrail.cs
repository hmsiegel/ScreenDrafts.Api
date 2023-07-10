namespace ScreenDrafts.Api.Persistence.Auditing;
public sealed class AuditTrail
{
    private readonly ISerializerService _serializer;

    public AuditTrail(EntityEntry entry, ISerializerService serializer)
    {
        Entry = entry;
        _serializer = serializer;
    }

    public EntityEntry Entry { get; }
    public Guid UserId { get; set; }
    public string? TableName { get; set; }
    public Dictionary<string, object?>? KeyValues { get; set; }
    public Dictionary<string, object?>? OldValues { get; set; }
    public Dictionary<string, object?>? NewValues { get; set; }
    public List<PropertyEntry>? TemporaryProperties { get; set; }
    public TrailType TrailType { get; set; }
    public List<string> ChangedColumns { get; set; }
    public bool HasTemporaryProperties => TemporaryProperties?.Any() == true;

    public Trail ToAuditTrail() =>
    new()
    {
        UserId = UserId,
        Type = TrailType.ToString(),
        TableName = TableName,
        CreatedAt = DateTime.UtcNow,
        PrimaryKey = _serializer.Serialize(KeyValues),
        OldValues = OldValues?.Count == 0 ? null : _serializer.Serialize(OldValues),
        NewValues = NewValues?.Count == 0 ? null : _serializer.Serialize(NewValues),
        AffectedColumns = ChangedColumns.Count == 0 ? null : _serializer.Serialize(ChangedColumns),
    };
}
