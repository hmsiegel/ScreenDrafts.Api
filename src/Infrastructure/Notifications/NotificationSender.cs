using static ScreenDrafts.Api.Shared.Notifications.NotificationConstants;

namespace ScreenDrafts.Api.Infrastructure.Notifications;
public class NotificationSender : INotificationSender
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationSender(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task BroadcastAsync(INotificationMessage notification, CancellationToken cancellationToken) =>
        _hubContext.Clients.All
            .SendAsync(
            NotificationFromServer,
            notification.GetType().FullName,
            notification,
            cancellationToken);

    public Task BroadcastAsync(INotificationMessage notification, IEnumerable<string> excludedConnectionIds, CancellationToken cancellationToken) =>
        _hubContext.Clients.AllExcept(excludedConnectionIds)
            .SendAsync(
            NotificationFromServer,
            notification.GetType().FullName,
            notification,
            cancellationToken);

    public Task SendToGroupAsync(INotificationMessage notification, string group, CancellationToken cancellationToken) =>
    _hubContext.Clients.Group(group)
        .SendAsync(NotificationFromServer, notification.GetType().FullName, notification, cancellationToken);

    public Task SendToGroupAsync(INotificationMessage notification, string group, IEnumerable<string> excludedConnectionIds, CancellationToken cancellationToken) =>
        _hubContext.Clients.GroupExcept(group, excludedConnectionIds)
            .SendAsync(NotificationFromServer, notification.GetType().FullName, notification, cancellationToken);

    public Task SendToGroupsAsync(INotificationMessage notification, IEnumerable<string> groupNames, CancellationToken cancellationToken) =>
        _hubContext.Clients.Groups(groupNames)
            .SendAsync(NotificationFromServer, notification.GetType().FullName, notification, cancellationToken);

    public Task SendToUserAsync(INotificationMessage notification, string userId, CancellationToken cancellationToken) =>
        _hubContext.Clients.User(userId)
            .SendAsync(NotificationFromServer, notification.GetType().FullName, notification, cancellationToken);

    public Task SendToUsersAsync(INotificationMessage notification, IEnumerable<string> userIds, CancellationToken cancellationToken) =>
        _hubContext.Clients.Users(userIds)
            .SendAsync(NotificationFromServer, notification.GetType().FullName, notification, cancellationToken);
}
