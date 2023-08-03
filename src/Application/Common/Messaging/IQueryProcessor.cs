namespace ScreenDrafts.Api.Application.Common.Messaging;
public interface IQueryProcessor
{
    TResult Process<TResult>(IQuery<TResult> query);
}
