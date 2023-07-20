namespace ScreenDrafts.Api.Domain.Primitives;
public interface IDomainEvent : INotification
{
    public DefaultIdType Id { get; init; }
}
