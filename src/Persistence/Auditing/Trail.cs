﻿namespace ScreenDrafts.Api.Persistence.Auditing;
public sealed class Trail : BaseEntity
{
    public DefaultIdType UserId { get; set; }
    public string? Type { get; set; }
    public string? TableName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? AffectedColumns { get; set; }
    public string? PrimaryKey { get; set; }
}
