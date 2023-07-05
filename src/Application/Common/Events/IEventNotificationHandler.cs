namespace ScreenDrafts.Api.Application.Common.Events;
public interface IEventNotificationHandler<TEvent> : INotificationHandler<EventNotification<TEvent>>
    where TEvent : IDomainEvent
{
}

public abstract class EventNotificationHandler<TEvent> : INotificationHandler<EventNotification<TEvent>>
    where TEvent : IDomainEvent
{
    public Task Handle(EventNotification<TEvent> notification, CancellationToken cancellationToken) =>
        Handle(notification.Event, cancellationToken);

    public abstract Task Handle(TEvent notification, CancellationToken cancellationToken);
}
