using Microsoft.AspNetCore.SignalR;
using SignalRServerApi.Helpers;
using System.Collections.Concurrent;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace SignalRServerApi.Controllers
{
    [Authorize]
    public class MiddlewareHub : Hub
    {
        private  readonly ILogger<object> _logger;
        public MiddlewareHub(ILogger<object> logger)
        {
            _logger = logger;
        }
        private static readonly ConcurrentDictionary<string, string> UserConnections = new();
        public override Task OnConnectedAsync()
        {
            _logger.LogInformation("Client Connected, Context:{0}", JsonSerializer.Serialize( this.Context.User));
            HttpContext httpContext =Context.GetHttpContext();
            //need to identify the connected client for logging only 
            object authUser = httpContext.Items["User"];
            _logger.LogInformation($"Connected User:{authUser}");
            
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
            _logger.LogInformation($"Disconnected :{exception}");
        }
        
        public async Task HandleNotificationFromClient()
        {
             
        }
        public async Task HandleActionAsync(RequestPayload payload)
        {
            switch (payload.ActionType)
            {
                case ActionType.Step1:
                    await DoStep1();
                    break;
                case ActionType.Step2:
                    await DoStep2();
                    break;
                case ActionType.Step3:
                    await DoStep3();
                    break;

            }

        }
        
        public async Task DoStep1()
        {
            ServerResponse response = new ServerResponse(){ Success=true ,Message="Step1 Done.",CurrentAction=ActionType.Step1};
            await Clients.Client(Context.ConnectionId).SendAsync(HubMessageType.InvokeClientAction,response);
        }
        public async Task DoStep2()
        {
            ServerResponse response = new ServerResponse() { Success = true, Message = "Step2 Done.", CurrentAction = ActionType.Step2 };
            await Clients.Client(Context.ConnectionId).SendAsync(HubMessageType.InvokeClientAction, response);

        }
        public async Task DoStep3()
        {
            ServerResponse response = new ServerResponse() { Success = true, Message = "Step3 Done.", CurrentAction = ActionType.Step3 };
            await Clients.Client(Context.ConnectionId).SendAsync(HubMessageType.InvokeClientAction, response);

        }

    }
}
