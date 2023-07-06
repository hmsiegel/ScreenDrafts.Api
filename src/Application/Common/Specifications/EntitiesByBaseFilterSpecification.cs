namespace ScreenDrafts.Api.Application.Common.Specifications;
public class EntitiesByBaseFilterSpecification<T, TResult> : Specification<T, TResult>
{
    public EntitiesByBaseFilterSpecification(BaseFilter filter) =>
        Query.SearchBy(filter);
}

public class EntitiesByBaseFilterSpecification<T>  : Specification<T>
{
    public EntitiesByBaseFilterSpecification(BaseFilter filter) =>
        Query.SearchBy(filter);
}
