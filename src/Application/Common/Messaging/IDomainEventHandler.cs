namespace ScreenDrafts.Api.Application.Common.Messaging;
public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
