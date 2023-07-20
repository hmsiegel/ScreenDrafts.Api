namespace ScreenDrafts.Api.Domain.Primitives;
public interface IHasDomainEvent
{
    public IReadOnlyList<IDomainEvent> DomainEvents { get; }
    public void ClearDomainEvents();
}
