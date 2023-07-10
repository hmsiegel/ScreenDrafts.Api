namespace ScreenDrafts.Api.Infrastructure.Notifications;

[Authorize]
public sealed class NotificationHub : Hub, ITransientService
{
    private readonly ILogger<NotificationHub> _logger;

    public NotificationHub(ILogger<NotificationHub> logger)
    {
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
        await base.OnConnectedAsync();
        _logger.LogInformation("Client connected: {connectionId}", Context.ConnectionId);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
        await base.OnDisconnectedAsync(exception);
        _logger.LogInformation("Client disconnected: {connectionId}", Context.ConnectionId);
    }
}
