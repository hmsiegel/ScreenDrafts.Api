namespace ScreenDrafts.Api.Infrastructure.Common.Services;
public class EventPublisher : IEventPublisher
{
    private readonly ILogger<EventPublisher> _logger;
    private readonly IPublisher _publisher;

    public EventPublisher(
        ILogger<EventPublisher> logger,
        IPublisher publisher)
    {
        _logger = logger;
        _publisher = publisher;
    }

    public Task PublishAsync(IDomainEvent @event)
    {
        _logger.LogInformation("Publishing event: {event}", @event.GetType().Name);
        return _publisher.Publish(CreateEventNotification(@event));
    }

    private static INotification CreateEventNotification(IDomainEvent @event)
    {
        return (INotification)Activator.CreateInstance(
                       typeof(EventNotification<>).MakeGenericType(@event.GetType()),
                       @event)!;
    }
}
