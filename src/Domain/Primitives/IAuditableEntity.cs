﻿namespace ScreenDrafts.Api.Domain.Primitives;
public interface IAuditableEntity
{
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}