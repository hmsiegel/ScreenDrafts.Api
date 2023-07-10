﻿namespace ScreenDrafts.Api.Application.Common.Persistence;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
