using ScreenDrafts.Api.Application.Common.Interfaces.Services;

namespace ScreenDrafts.Api.Application.Common.Events;
public interface IEventPublisher : ITransientService
{
    Task PublishAsync(IDomainEvent @event);
}
