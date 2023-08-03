﻿namespace ScreenDrafts.Api.Application.Common.Processing;
internal sealed class QueryProcessor : IQueryProcessor
{
    private readonly IServiceProvider _serviceProvider;

    public QueryProcessor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TResult Process<TResult>(IQuery<TResult> query)
    {
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        dynamic handler = _serviceProvider.GetRequiredService(handlerType);

        return handler.Handle((dynamic)query);
    }
}
