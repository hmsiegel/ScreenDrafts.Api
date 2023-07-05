namespace ScreenDrafts.Api.Application.Common.Events;
public interface IEventPublisher : ITransientService
{
    Task PublishAsync(IDomainEvent @event);
}
