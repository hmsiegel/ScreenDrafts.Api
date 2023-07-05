namespace ScreenDrafts.Api.Application.Common.Events;
public class EventNotification<TEvent> : INotification
       where TEvent : IDomainEvent
{
    public EventNotification(TEvent @event) => Event = @event;

    public TEvent Event { get; }
}
