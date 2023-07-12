﻿namespace ScreenDrafts.Api.Domain.DrafterEntity;
public sealed class Drafter : Entity, IAuditableEntity
{
    private readonly List<Draft> _drafts = new();

    private Drafter(
        string id,
        bool hasRolloverVeto = false,
        bool hasRolloverVetooverride = false)
        : base(id)
    {
        HasRolloverVeto = hasRolloverVeto;
        HasRolloverVetooverride = hasRolloverVetooverride;
    }

    private Drafter()
    {
    }

    public ApplicationUser? User { get; set; }
    public string? UserId { get; set; }
    public bool? HasRolloverVeto { get; private set; }
    public bool? HasRolloverVetooverride { get; private set; }
    public IReadOnlyList<Draft>? Drafts => _drafts.AsReadOnly();

    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DefaultIdType CreatedBy { get; set; }
    public DefaultIdType? ModifiedBy { get; set; }

    public static Drafter Create(string id)
    {
        return new Drafter(id);
    }

    public void AddRolloverVeto() => HasRolloverVeto = true;

    public void RemoveRolloverVeto() => HasRolloverVeto = false;

    public void AddRolloverVetooverride() => HasRolloverVetooverride = true;

    public void RemoveRolloverVetooverride() => HasRolloverVetooverride = false;
}
