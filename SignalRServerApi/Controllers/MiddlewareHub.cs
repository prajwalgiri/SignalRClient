using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace SignalRServerApi.Controllers
{
    public class MiddlewareHub : Hub
    {
        private static readonly ConcurrentDictionary<string, string> UserConnections = new();
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = UserConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (user != null)
            {
                UserConnections.TryRemove(user, out _);
                await Clients.All.SendAsync("UserDisconnected", user);
            }
            await base.OnDisconnectedAsync(exception);
        }
        
        public async Task HandleAsync(string fromUser, string toUser, string message)
        {
            if (UserConnections.TryGetValue(toUser, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("ReceivePrivateMessage", fromUser, message);
            }
            else
            {
                await Clients.Caller.SendAsync("Error", $"User '{toUser}' is not online.");
            }
        }
    }
}
