﻿namespace ScreenDrafts.Api.Domain.Identity;
public sealed class ApplicationRole : IdentityRole<DefaultIdType>
{
    public ApplicationRole(string name, string? description = null)
        : base(name)
    {
        Description = description;
        NormalizedName = name.ToUpperInvariant();
    }

    public string? Description { get; set; }
}
