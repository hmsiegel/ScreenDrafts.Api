namespace ScreenDrafts.Api.Application.Common.Specifications;
public class EntitiesByPaginationFilterSpecification<T, TResult> : EntitiesByBaseFilterSpecification<T, TResult>
    where T : class
    where TResult : class
{
    public EntitiesByPaginationFilterSpecification(PaginationFilter filter)
        : base(filter) => Query.PaginateBy(filter);
}

public class EntitiesByPaginationFilterSpecification<T> : EntitiesByBaseFilterSpecification<T>
    where T : class
{
    public EntitiesByPaginationFilterSpecification(PaginationFilter filter)
        : base(filter) => Query.PaginateBy(filter);
}
